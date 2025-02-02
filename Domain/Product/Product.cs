using System.ComponentModel.DataAnnotations;
using System.Text.Json;

//TODO: add an EntityBase class to both show inheritance
// and how useful base entities are with EF
namespace Domain.Product {
    public class Product :BaseEntity {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public int Quantity { get; set; } = 0;
        public decimal Price { get; set; } = 0.0M;
    }
}
