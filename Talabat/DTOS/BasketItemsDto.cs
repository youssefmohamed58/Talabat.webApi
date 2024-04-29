using System.ComponentModel.DataAnnotations;
using Talabat.Core.Entities;

namespace Talabat.Apis.DTOS
{
    public class BasketItemsDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string PictureUrl { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        [Range(0.1, double.MaxValue , ErrorMessage ="Price can not be zero")]
        public decimal Price { get; set; }
        [Required]
        [Range(0.1, int.MaxValue, ErrorMessage = "Quantity must be one item at least")]
        public int Quantity { get; set; }
    }
}
