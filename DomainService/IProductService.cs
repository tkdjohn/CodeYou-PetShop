using Domain.Product;

namespace DomainService {
    public interface IProductService {
        Task<Product> AddProductAsync<T>(Product product) where T : Product;
        Task RemoveProductAsync(Product product);
        Task<Product?> GetProductAsync(int id);
        Task<Product?> GetProductAsync(string name);
        Task<List<Product>> GetProductsAsync();
        Task<List<Product>> GetInStockProductsAsync();
        Task<decimal> GetTotalPriceOfInventoryAsync();
        Task UpdateProduct(Product productUpdate);
        Task<bool> ProductExists(int Id);
    }
}