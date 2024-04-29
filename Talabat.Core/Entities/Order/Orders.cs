using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Order
{
    public class Orders : BaseEntity
    {

        public Orders()
        {
        }
        public Orders(string buyerEmail, Address shippingAddress, DelivaryMethod delivaryMethod, ICollection<OrderItem> items, decimal subTotal ,string paymentIntendId)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            DelivaryMethod = delivaryMethod;
            Items = items;
            SubTotal = subTotal;
            PaymentIntentId = paymentIntendId;
        }


        public string BuyerEmail {  get; set; }

        public DateTimeOffset OrderDate { get; set;} = DateTimeOffset.Now;

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public Address ShippingAddress {  get; set; }

        public DelivaryMethod DelivaryMethod { get; set; }

        public ICollection<OrderItem> Items { get; set;} = new HashSet<OrderItem>();   

        public decimal SubTotal { get; set; }

        public decimal GetTotal () => SubTotal + DelivaryMethod.Cost;

        public string? PaymentIntentId {  get; set; }


    }
}
