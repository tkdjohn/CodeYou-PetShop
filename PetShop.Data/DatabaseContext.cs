using Microsoft.EntityFrameworkCore;
using PetShop.DomainEntities;

namespace PetShop.Data {
    public class DatabaseContext : DbContext, IDatabaseContext {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        //Note that there is no DbSet for OrderProducts here. That's because any 
        // manipulation of OrderProducts should happen through the Order repository
        // and therefore gets saved along with and as part of the Orders DbSet.

        public DatabaseContext(DbContextOptions<DatabaseContext> options ): base(options) {
            
        }
        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options) { 
            
        }

        public Task SaveChangesAsync() {
            return base.SaveChangesAsync();
        }
    }
}
