using Data;
using DomainEntities;
using DomainEntities.Validators;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace DomainService {
    public class ProductService(ILogger logger, IProductRepository productDb) : IProductService {
        // This readonly means we can't assign a new List to 
        // products. It doesn't mean we can't call
        // products.Add() or _products.Remove()
        private readonly IProductRepository products = productDb;
        private readonly ILogger logger = logger;

        public async Task<Product> AddProductAsync<T>(Product product) where T : Product {
            ArgumentNullException.ThrowIfNull(product);
            product.ValidateAndThrow<T>();
            logger.LogDebug("Adding a {Type}", typeof(T));
            return await products.AddAsync(product).ConfigureAwait(false);
        }

        public async Task RemoveProductAsync(Product product) {
            await products.RemoveAsync(product).ConfigureAwait(false);
        }

        public async Task<Product?> GetProductAsync(int id) {
            return await products.GetAsync(id).ConfigureAwait(false);
        }
        
        public async Task<Product?> GetProductAsync(string name) {
            return await products.GetAsync(name).ConfigureAwait(false);
        }

        public async Task<List<Product>> GetProductsAsync() {
            return await products.GetAsync().ConfigureAwait(false);
        }

        public async Task<List<Product>> GetInStockProductsAsync() {
            return await products.GetInStockAsync().ConfigureAwait(false);
        }

        public async Task<decimal> GetTotalPriceOfInventoryAsync() {
            var inStock = await products.GetInStockAsync().ConfigureAwait(false);
            return inStock.Select(p => p.Price * p.Quantity).Sum();
        }

        public async Task UpdateProduct(Product productUpdate) {
            productUpdate.ValidateAndThrow();
            await products.UpdateAsync(productUpdate).ConfigureAwait(false);
        }

        public async Task<bool> ProductExists(int id) {
            return await products.GetAsync(id).ConfigureAwait(false) != null;
        }
    }
}
