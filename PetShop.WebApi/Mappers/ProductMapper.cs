using PetShop.DomainEntities;
using PetShop.WebApi.Requests;
using PetShop.WebApi.Responses;

namespace PetShop.WebApi.Mappers {
    public static class ProductMapper {
        public static ProductResponseModel ToProductResponseModel(this Product product) {
            return new ProductResponseModel() {
                Description = product.Description,
                LastModifiedDate = product.LastUpdatedDate,
                Name = product.Name,
                Price = product.Price,
                ProductId = product.ProductId,
                Quantity = product.Quantity
            };
        }

        public static ProductResponseModel[] ToProductResponseModelArray(this List<Product> products) {
            return products.ToList().Select(p => p.ToProductResponseModel()).ToArray();
        }

        public static Product ToProduct(this ProductRequestModel model) {
            return new Product {
                Description = model.Description,
                Name = model.Name,
                Price = model.Price,
                Quantity = model.Quantity
            };
        }
    }
}
