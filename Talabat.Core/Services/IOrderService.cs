using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order;

namespace Talabat.Core.Services
{
    public interface IOrderService
    {
        Task<Orders?> CreateOrderAsync(string BuyerEmail , string BasketId , int DeliveryMethodId , Address ShippingAddress);
        Task<IReadOnlyList<Orders>> GetOrdersForSpecificUserAsync(string BuyerEmail);

        Task<Orders> GetOrderByIdForSpecificUserAsync(string BuyerEmail, int OrderId);
    }
}
