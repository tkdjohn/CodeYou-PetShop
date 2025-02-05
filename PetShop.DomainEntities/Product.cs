using System.ComponentModel.DataAnnotations;

namespace PetShop.DomainEntities {
    public class Product : EntityBase {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        [Required]
        public int Quantity { get; set; } = 0;
        [Required]
        public decimal Price { get; set; } = 0.0M;
    }
}
