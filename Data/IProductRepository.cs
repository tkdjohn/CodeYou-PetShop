using DomainEntities;

namespace Data {
    public interface IProductRepository {
        Task<Product> AddAsync(Product product);
        Task RemoveAsync(Product product);
        Task<Product?> GetAsync(int productId);
        Task<Product?> GetAsync(string name);
        Task<List<Product>> GetAsync();
        Task<List<Product>> GetInStockAsync();
        Task UpdateAsync(Product productUpdate);
        Task<bool> ProductExists(int productId);
    }
}