using PetShop.DomainEntities;

namespace PetShop.DomainService {
    public interface IProductService {
        Task<Product> AddProductAsync(Product product);
        Task RemoveProductAsync(Product product);
        Task<Product?> GetProductAsync(int id);
        Task<Product?> GetProductAsync(string name);
        Task<List<Product>> GetProductsAsync(bool InStockOnly = false);
        Task<List<Product>> GetInStockProductsAsync();
        Task<decimal> GetTotalPriceOfInventoryAsync();
        Task UpdateProduct(Product productUpdate);
        Task<bool> ProductExists(int Id);
    }
}