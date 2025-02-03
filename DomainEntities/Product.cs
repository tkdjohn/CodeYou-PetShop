using System.ComponentModel.DataAnnotations;
using System.Text.Json;

//TODO: add an EntityBase class to both show inheritance
// and how useful base entities are with EF
namespace DomainEntities {
    public class Product : BaseEntity {
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
