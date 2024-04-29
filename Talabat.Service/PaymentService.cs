using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Stripe;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order;
using Talabat.Core.Repositories;
using Talabat.Core.Services;
using Product = Talabat.Core.Entities.Product;

namespace Talabat.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketRepository _basketRepository;

        public PaymentService(IConfiguration configration , IUnitOfWork unitOfWork , IBasketRepository basketRepository)
        {
            _configration = configration;
            _unitOfWork = unitOfWork;
            _basketRepository = basketRepository;
        }
        public async Task<CustomerBasket?> CreateOrUpdatePaymentIntendId(string BasketId)
        {
            //SecretKey

            StripeConfiguration.ApiKey = _configration["SecretKeys:SecretKey"];

            // Get Basket

            var Basket = await _basketRepository.GetCustomerBasket(BasketId);
            if (Basket == null)
                return null;

            // Get true Price Of Product

            if (Basket.items.Count > 0)
            {
                foreach(var item in Basket.items) 
                {
                    var product = await _unitOfWork.Repositories<Product>().GetByIdAsync(item.Id);
                    if(item.Price!=product.Price)
                        item.Price = product.Price;
                }
            }
            // TatalCost = SubTotal + DelivaryMethodCost

            var shippingCost = 0M;
            if (Basket.DelivaryMethodId != null)
            {
                var DelivaryMethod = await _unitOfWork.Repositories<DelivaryMethod>().GetByIdAsync(Basket.DelivaryMethodId);
                shippingCost = DelivaryMethod.Cost;
            }
             var Subtotal = Basket.items.Sum(item => item.Price * item.Quantity);
            var Service = new PaymentIntentService();

            // Create PaymentIntent

            if (string.IsNullOrEmpty(Basket.PaymentIntentId))
            {
                var Options = new PaymentIntentCreateOptions()
                {
                    Amount = (long)Subtotal * 100 + (long)shippingCost * 100,
                    Currency = "Usd",
                    PaymentMethodTypes = new List<string> {"card"}
                };
                PaymentIntent paymentIntent = await Service.CreateAsync(Options);
                Basket.PaymentIntentId = paymentIntent.Id;
                Basket.ClientSecret = paymentIntent.ClientSecret;   
            }

            else 
            {
                // Update PaymentIntent
                var Options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)Subtotal * 100 + (long)shippingCost * 100,
                };
                PaymentIntent paymentIntent = await Service.UpdateAsync(Basket.PaymentIntentId ,Options );
                Basket.PaymentIntentId = paymentIntent.Id;
                Basket.ClientSecret = paymentIntent.ClientSecret;
            }
            
            await _basketRepository.CreateOrUpdateBasket(Basket); 

            return Basket;  

        }
    }
}
