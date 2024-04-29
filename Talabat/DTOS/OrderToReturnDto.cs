using Talabat.Core.Entities.Order;

namespace Talabat.Apis.DTOS
{
    public class OrderToReturnDto
    {
        public int Id { get; set; } 
        public string BuyerEmail { get; set; }

        public DateTimeOffset OrderDate { get; set; } 

        public string Status { get; set; } 

        public Address ShippingAddress { get; set; }

        public string DelivaryMethod { get; set; }

        public decimal DelivaryMethodCost { get; set; }

        public ICollection<OrderItemDto> Items { get; set; } = new HashSet<OrderItemDto>();    


        public decimal SubTotal { get; set; }

        public decimal Total {  get; set; }

        public string PaymentIntentId {  get; set; }
    }
}
