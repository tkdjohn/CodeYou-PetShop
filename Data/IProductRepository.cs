using Domain.Product;

namespace Data {
    public interface IProductRepository {
        Task<Product> AddAsync(Product product);
        Task RemoveAsync(Product product);
        Task<Product?> GetProductAsync(int productId);
        Task<Product?> GetProductAsync(string name);
        Task<List<Product>> GetProductsAsync();
        Task<List<Product>> GetInStockProductsAsync();
        Task UpdateProduct(Product productUpdate);
    }
}