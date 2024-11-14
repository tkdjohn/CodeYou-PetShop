using System.Text.Json;

namespace PetShop.Product
{
    public class Product
    {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public int Qty { get; set; } = 0;
        public decimal Price { get; set; } = 0.0M;

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
