using Domain.Product;
using Microsoft.EntityFrameworkCore;

namespace Data {
    public class PetShopDbContext : DbContext, IPetShopDbContext {
        public DbSet<Product> Products { get; set; }

        public string DbPath { get; }

        public PetShopDbContext() {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "PetShop.db");
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

        public Task SaveChangesAsync() {
           return base.SaveChangesAsync();
        }
    }
}
