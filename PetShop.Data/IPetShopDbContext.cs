using Microsoft.EntityFrameworkCore;
using PetShop.DomainEntities;

namespace PetShop.Data {
    public interface IPetShopDbContext {
        string DbPath { get; }
        DbSet<Order> Orders { get; }
        DbSet<Product> Products { get; }

        Task SaveChangesAsync();
    }
}