using DomainEntities;
using Microsoft.EntityFrameworkCore;

namespace Data {
    public interface IPetShopDbContext  {
        string DbPath { get; }
        DbSet<Product> Products { get; set; }
        DbSet<Order> Orders { get; set; }

        Task SaveChangesAsync();
    }
}