using DomainEntities;

namespace Data {
    public interface IOrderRepository {
        Task<Order> AddAsync(Order order);
        Task<List<Order>> GetAsync();
        Task<Order?> GetAsync(int orderId);
        Task RemoveAsync(Order order);
        Task UpdateAsync(Order orderUpdate);
        Task<bool> OrderExists(int orderId);
    }
}