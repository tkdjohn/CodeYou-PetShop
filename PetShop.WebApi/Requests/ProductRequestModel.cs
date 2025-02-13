using System.ComponentModel.DataAnnotations;

namespace PetShop.WebApi.Requests {
    public class ProductRequestModel {
        [Required]
        public required string Name { get; set; }
        public required string Description { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
        [Required]
        [Range(0.01, 1000000)]
        public decimal Price { get; set; }
    }
}
