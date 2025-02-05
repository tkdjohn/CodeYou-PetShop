using Microsoft.EntityFrameworkCore;
using PetShop.DomainEntities;

namespace PetShop.Data {
    public class PetShopDbContext : DbContext, IPetShopDbContext {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        //Note that there is no list for OrderProducts here. That's because any 
        // manipulation of OrderProducts should happen through the Order repository.
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
