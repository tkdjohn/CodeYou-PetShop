using System.ComponentModel.DataAnnotations;

namespace PetShop.WebApi.Responses {
    public class ProductResponseModel {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } 
        public int Quantity { get; set; } 
        public decimal Price { get; set; }

        public DateTime LastModifiedDate { get; set; }
        // but we'll keep the rest of the audit info out of here.
    }
}
