using System.ComponentModel.DataAnnotations;

namespace PetShop.WebApi.Responses {
    public class ProductResponseModel {
        public int ProductId { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; } 
        public required int Quantity { get; set; } 
        public required decimal Price { get; set; }

        public DateTime LastModifiedDate { get; set; }
        // but we'll keep the rest of the audit info out of here.
    }
}
