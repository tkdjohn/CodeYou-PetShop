using Domain.Product;
using Microsoft.EntityFrameworkCore;

namespace Data {
    public class ProductRepository : IProductRepository {
        private readonly IPetShopDbContext petShopDb;

        public ProductRepository(IPetShopDbContext petShopDb) {
            this.petShopDb = petShopDb;
        }

        public async Task<Product> AddAsync(Product product) {
            var dbRow = await petShopDb.Products.AddAsync(product).ConfigureAwait(false);
            await petShopDb.SaveChangesAsync().ConfigureAwait(false);
            return dbRow.Entity;
        }
        public async Task RemoveAsync(Product product) {
            petShopDb.Products.Remove(product);
            await petShopDb.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<Product?> GetProductAsync(int productId) {
            return await petShopDb.Products.FindAsync(productId).ConfigureAwait(false);
        }

        public async Task<Product?> GetProductAsync(string name) {
            //TODO: *jws* FirstOrDefault will return the first if it exists, but
            // we should really handle the case where multiple products have the same 
            // name.
            return await petShopDb.Products.Where(p => p.Name == name).FirstOrDefaultAsync();
        }

        public async Task<List<Product>> GetProductsAsync() {
            // what we don't wan to do is return petShopDb.Products which would 
            // give the caller direct access to the database so we'll call .ToList()
            // to get a List instead. 
            return await petShopDb.Products.ToListAsync().ConfigureAwait(false);
        }

        // There is likely a way to gain some code reuse by making a more generic search
        // available. 
        public async Task<List<Product>> GetInStockProductsAsync() {
            return await petShopDb.Products.Where(p => p.Quantity > 0).ToListAsync().ConfigureAwait(false);
        }

        public async Task UpdateProductAsync(Product productUpdate) {
            ArgumentNullException.ThrowIfNull(productUpdate);
            var existingProduct = await petShopDb.Products.FindAsync(productUpdate.Id).ConfigureAwait(false) 
                ?? throw new InvalidOperationException($"Product Id {productUpdate.Id} not found.");

            existingProduct.Name = productUpdate.Name;
            existingProduct.Price = productUpdate.Price;
            existingProduct.Quantity = productUpdate.Quantity;
            existingProduct.Description = productUpdate.Description;
            // TODO: need to force LastUpdatedDate to always get updated somehow.
            existingProduct.LastUpdatedDate = DateTime.Now;
            await petShopDb.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
