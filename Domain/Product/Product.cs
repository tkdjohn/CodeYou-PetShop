using System.Text.Json;

namespace Domain.Product {
    public class Product {
        public int ProductId { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public int Quantity { get; set; } = 0;
        public decimal Price { get; set; } = 0.0M;
    }
}
