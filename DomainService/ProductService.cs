using Data;
using Domain.Product;
using Domain.Product.Validators;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace DomainService {
    public class ProductService : IProductService {
        // This readonly means we can't assign a new List to 
        // products. It doesn't mean we can't call
        // products.Add() or _products.Remove()
        private readonly IProductRepository products;
        private ILogger logger;

        //public bool SkipTheDictionaries { get; set; } = true;
        public ProductService(ILogger logger, IProductRepository productDb) {
            this.logger = logger;
            this.products = productDb;
        }

        public async Task<Product> AddProductAsync<T>(Product product) where T : Product {
            ArgumentNullException.ThrowIfNull(product);
            product.ValidateAndThrow<T>();
            logger.LogDebug($"Adding a {typeof(T)}");
            return await products.AddAsync(product).ConfigureAwait(false);
        }

        public async Task RemoveProductAsync(Product product) {
            await products.RemoveAsync(product).ConfigureAwait(false);
        }

        public async Task<Product?> GetProductAsync(int id) {
            return await products.GetProductAsync(id).ConfigureAwait(false);
        }
        
        public async Task<Product?> GetProductAsync(string name) {
            return await products.GetProductAsync(name).ConfigureAwait(false);
        }

        public async Task<List<Product>> GetProductsAsync() {
            return await products.GetProductsAsync().ConfigureAwait(false);
        }

        public async Task<List<Product>> GetInStockProductsAsync() {
            return await products.GetInStockProductsAsync().ConfigureAwait(false);
        }

        public async Task<decimal> GetTotalPriceOfInventoryAsync() {
            var inStock = await products.GetInStockProductsAsync().ConfigureAwait(false);
            return inStock.Select(p => p.Price * p.Quantity).Sum();
        }

        public async Task UpdateProduct(Product productUpdate) {
            await products.UpdateProduct(productUpdate).ConfigureAwait(false);
        }

        public async Task<bool> ProductExists(int id) {
            return await products.GetProductAsync(id).ConfigureAwait(false) != null;
        }
    }
}
