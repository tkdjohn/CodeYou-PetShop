using Microsoft.EntityFrameworkCore;
using PetShop.DomainEntities;

namespace PetShop.Data {
    public interface IDatabaseContext {
        DbSet<Order> Orders { get; }
        DbSet<Product> Products { get; }

        Task SaveChangesAsync();
    }
}