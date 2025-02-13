using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using PetShop.Data;
using PetShop.DomainService;
using System.Reflection;

namespace PetShop.WebApi {
    public class Program {

        public static void Main(string[] args) {
            // this is the code that was created by default when we added
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

            builder.Services.AddDbContext<IDatabaseContext, DatabaseContext>(options => {
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

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseRouting();
            app.UseHttpsRedirection();
            //app.UseAuthentication();
            //app.UseAuthorization();

            app.MapControllers();

            app.Run();

        }
    }
}