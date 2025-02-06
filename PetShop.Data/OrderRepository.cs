using Microsoft.EntityFrameworkCore;
using PetShop.DomainEntities;

namespace PetShop.Data {
    public class OrderRepository(IDatabaseContext petShopDb) : IOrderRepository {
        private readonly IDatabaseContext petShopDb = petShopDb;

        public async Task<Order> AddAsync(Order order) {
            var dbRow = await petShopDb.Orders.AddAsync(order).ConfigureAwait(false);
            await petShopDb.SaveChangesAsync().ConfigureAwait(false);
            return dbRow.Entity;
        }

        public async Task RemoveAsync(Order order) {
            // TODO: does this remove associated order items as well?
            petShopDb.Orders.Remove(order);
            await petShopDb.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<Order?> GetAsync(int orderId) {
            return await petShopDb.Orders
                .Include(x => x.OrderProducts)
                .FirstOrDefaultAsync(o => o.OrderId == orderId)
                .ConfigureAwait(false);
        }

        public async Task<List<Order>> GetAsync() {
            // what we don't wan to do is return petShopDb.Orders which would 
            // give the caller direct access to the database so we'll call .ToList()
            // to get a List instead. 
            return await petShopDb.Orders
                .Include(x => x.OrderProducts)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task UpdateAsync(Order orderUpdate) {
            ArgumentNullException.ThrowIfNull(nameof(orderUpdate));
            var existingOrder = await petShopDb.Orders.FindAsync(orderUpdate.OrderId).ConfigureAwait(false)
                ?? throw new InvalidOperationException($"Order Id {orderUpdate.OrderId} not found");

            existingOrder.OrderDate = orderUpdate.OrderDate;
            // TODO: this is actually bad and will likely leave orphaned OrderProducts
            // in the database
            existingOrder.OrderProducts = orderUpdate.OrderProducts;
            existingOrder.LastUpdatedDate = DateTime.Now;
            await petShopDb.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<bool> OrderExists(int orderId) {
            return await petShopDb.Orders.AnyAsync(x => x.OrderId == orderId).ConfigureAwait(false);
        }
    }
}
