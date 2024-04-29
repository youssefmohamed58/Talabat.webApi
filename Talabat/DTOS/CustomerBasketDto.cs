using System.ComponentModel.DataAnnotations;
using Talabat.Core.Entities;

namespace Talabat.Apis.DTOS
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; }

        public List<BasketItemsDto> items { get; set; }

        public string? ClientSecret { get; set; }

        public string? PaymentIntentId { get; set; }

        public int? DelivaryMethodId { get; set; }

    }
}
