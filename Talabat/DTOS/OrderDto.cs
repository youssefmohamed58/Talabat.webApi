using System.ComponentModel.DataAnnotations;
using Talabat.Core.Entities.Order;

namespace Talabat.Apis.DTOS
{
    public class OrderDto
    {
        [Required]
        public int DelivaryMethodId { get; set; }

        public string BasketId {  get; set; }

        public AddressDto shippingAddress { get; set; }
    }
}
