using Domain.Product;
using Microsoft.EntityFrameworkCore;

namespace Data {
    public interface IPetShopDbContext  {
        string DbPath { get; }
        DbSet<Product> Products { get; set; }

        Task SaveChangesAsync();
    }
}