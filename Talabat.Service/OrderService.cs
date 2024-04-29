using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order;
using Talabat.Core.Repositories;
using Talabat.Core.Services;
using Talabat.Core.Specification.OrderSpecification;

namespace Talabat.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository basketRepo;
        private readonly IUnitOfWork unitofwork;
        private readonly IPaymentService _paymentService;

        public OrderService(IBasketRepository basketRepository, IUnitOfWork _unitofwork , IPaymentService paymentService)
        {
            basketRepo = basketRepository;
            unitofwork = _unitofwork;
           _paymentService = paymentService;
        }
        public async Task<Orders?> CreateOrderAsync(string BuyerEmail, string BasketId, int DeliveryMethodId, Address ShippingAddress)
        {
            // Get Basket
            var Basket = await basketRepo.GetCustomerBasket(BasketId);

            // Get Items from Basket
            var OrderItems = new List<OrderItem>();

            if(Basket?.items.Count > 0)
            {
                foreach(var item in Basket.items)
                {
                    
                    var Product = await unitofwork.Repositories<Product>().GetByIdAsync(item.Id);
                    var ProductItemOrders = new ProductItemOrdered(Product.Id , Product.Name, Product.PictureUrl);
                    var ItemsForOrder = new OrderItem(ProductItemOrders , Product.Price , item.Quantity);

                    OrderItems.Add(ItemsForOrder);

                }
            }
            //calculate subtotal

            var subtotal = OrderItems.Sum(item => item.price * item.quantity);

            // Get DelivaryMethod

            var DelivaryMethod = await unitofwork.Repositories<DelivaryMethod>().GetByIdAsync(DeliveryMethodId);

            // Create Order
            // check if PaymentIntendId is Exist or not in other Order
            var spec = new OrderWithPaymentIntentSpec(Basket.PaymentIntentId);
            var ExistOrder = await unitofwork.Repositories<Orders>().GetByIdWithSpecAsync(spec);
            if (ExistOrder != null)
                unitofwork.Repositories<Orders>().Delete(ExistOrder);
              await _paymentService.CreateOrUpdatePaymentIntendId(BasketId); 
                

            var Order = new Orders(BuyerEmail , ShippingAddress , DelivaryMethod , OrderItems , subtotal , Basket.PaymentIntentId);

            await unitofwork.Repositories<Orders>().AddAsync(Order);
            // save order to database
            var Result = await unitofwork.CompleteAsync();
            if (Result <= 0)
                return null;
            return Order;

        }

        public async Task<Orders> GetOrderByIdForSpecificUserAsync(string BuyerEmail, int OrderId)
        {
            var spec = new OrderSpecifications(BuyerEmail , OrderId);
            var Orders = await unitofwork.Repositories<Orders>().GetByIdWithSpecAsync(spec);
            return Orders;

        }

        public async Task<IReadOnlyList<Orders>> GetOrdersForSpecificUserAsync(string BuyerEmail)
        {
            var spec = new OrderSpecifications(BuyerEmail);
            var Orders = await unitofwork.Repositories<Orders>().GetAllWithSpecAsync(spec);
            return Orders;
        }
    }
}
