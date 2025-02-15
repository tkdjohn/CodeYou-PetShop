using Microsoft.EntityFrameworkCore;
using PetShop.DomainEntities;

namespace PetShop.Data {
    public class DatabaseContext : DbContext, IDatabaseContext {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        //Note that there is no DbSet for OrderProducts here. That's because any 
        // manipulation of OrderProducts should happen through the Order repository
        // and therefore gets saved along with and as part of the Orders DbSet.

        public DatabaseContext() {
            //  needed by ef cli tools 
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options ): base(options) {
            // needed to properly inject DBContext at runtime
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) {
            options.UseSqlite($"Data Source={GetSqliteDbPath()}");
        }

        public static string GetSqliteDbPath() {
            // The following configures EF to create a Sqlite database file in the
            // special "local" folder for your platform.
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            return Path.Join(path, "PetShop.db");
        }

        public Task SaveChangesAsync() {
            return base.SaveChangesAsync();
        }
    }
}
