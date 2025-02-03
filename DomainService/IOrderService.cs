using DomainEntities;

namespace DomainService {
    public interface IOrderService {
        Task<Order> AddOrderAsync(Order order);
        Task RemoveOrderAsync(Order order);
        Task<Order?> GetOrderAsync(int id);
        Task<List<Order>> GetOrdersAsync();
        Task UpdateOrder(Order order);
        Task<bool> OrderExists(int orderId);
    }
}