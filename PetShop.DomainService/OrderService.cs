using Microsoft.Extensions.Logging;
using PetShop.Data;
using PetShop.DomainEntities;
using PetShop.DomainEntities.Validators;

namespace PetShop.DomainService {
    public class OrderService(ILogger logger, IOrderRepository orderDb) : IOrderService {
        private readonly IOrderRepository orders = orderDb;
        private readonly ILogger logger = logger;

        public async Task<Order> AddOrderAsync(Order order) {
            ArgumentNullException.ThrowIfNull(order);
            order.ValidateAndThrow();
            order = await orders.AddAsync(order).ConfigureAwait(false);
            logger.LogDebug("Added Order # {OrderId}", order.OrderId);
            return order;
        }

        public async Task RemoveOrderAsync(Order order) {
            await orders.RemoveAsync(order).ConfigureAwait(false);
        }

        public async Task<Order?> GetOrderAsync(int id) {
            return await orders.GetAsync(id).ConfigureAwait(false);
        }

        public async Task<List<Order>> GetOrdersAsync() {
            return await orders.GetAsync().ConfigureAwait(false);
        }

        public async Task UpdateOrder(Order orderUpdate) {
            orderUpdate.ValidateAndThrow();
            await orders.UpdateAsync(orderUpdate).ConfigureAwait(false);
        }

        public async Task<bool> OrderExists(int id) {
            return await orders.OrderExists(id).ConfigureAwait(false);
        }
    }
}
