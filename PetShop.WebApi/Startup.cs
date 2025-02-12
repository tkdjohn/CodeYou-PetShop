using Microsoft.EntityFrameworkCore;
using PetShop.Data;
using PetShop.DomainService;

namespace PetShop.WebApi {
    public class Startup {

        public void ConfigureServices(IServiceCollection services) {
            // this code should look a bit familiar from our CLI Program.cs ConfigureServices method
            services.AddDbContext<IDatabaseContext, DatabaseContext>(options => {
                // The following configures EF to create a Sqlite database file in the
                // special "local" folder for your platform.
                var folder = Environment.SpecialFolder.LocalApplicationData;
                var path = Environment.GetFolderPath(folder);
                var DbPath = Path.Join(path, "PetShop.db");
                options.UseSqlite($"Data Source={DbPath}");
            })
            .AddScoped<IProductRepository, ProductRepository>()
            .AddScoped<IOrderRepository, OrderRepository>()
            .AddScoped<IProductService, ProductService>()
            .AddScoped<IOrderService, OrderService>()
            .AddLogging(options => {
                options.AddDebug();
                options.SetMinimumLevel(LogLevel.Debug);
                options.AddSimpleConsole(options => {
                    options.SingleLine = true;
                    options.TimestampFormat = "HH:mm:ss.fff ";
                    options.ColorBehavior = Microsoft.Extensions.Logging.Console.LoggerColorBehavior.Enabled;
                });
            });
            //Note how we can chain calls to .AddXXXXXX() or call them separately using services.AddXXXXX()

            services.AddControllers();
        }

        // unused in our code (this is more or less duplicate code from what's in 
        public void Configure(IApplicationBuilder app, IHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            //app.UseAuthentication();
            //app.UseAuthorization();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
