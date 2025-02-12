using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using PetShop.Data;
using PetShop.DomainService;
using PetShop.WebApi;
using System.Reflection;

public class Program {

    public static void Main(string[] args) {
        // samples of calling the older ways of creating hosts for our API.
        //CreateHostBuilder(args).Build().Run();
        //CreateWebHostBuilder(args).Build().Run();

        // this is the code that was created by default when we added
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();

        // TALK about swagger
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

    static IHostBuilder CreateHostBuilder(string[] args) {
        // creates a generic Host - which can still be used for
        // hosting REST Web APIs but can host services that are not necessarily 
        // WEB specific (ie do not have to use the HTTP protocol, though they 
        // often still do.
        // this was new as of ASP.Net Core 3
        // Configuration of the host is generally done in the Startup class
        return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => {
                webBuilder.UseStartup<Startup>();
            });
    }

    static IWebHostBuilder CreateWebHostBuilder(string[] args) {
        // creates a WebHost - which is more specific to the HTTP protocols 
        // basically this is a generic Host with Web specific stuff added.
        // this was new with ASP.Net Core 2.1  (careful this isn't the same as .NetCore 2.1 !!!!!)
        // This is what is shown in the puluralsight videos.
        // Configuration of the host is generally done in the Startup class.
        return WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();
    }

    // a note on ASP.Net Core versions:
    // first recognize that ASP.Net Core is not the same thing as dotNet Core. 
    // https://en.wikipedia.org/wiki/ASP.NET_Core
    // 3.0 was released in 2019 quickly followed by 3.1
    // after that they started using what microsoft calls "parallel versioning"
    // which is where the version numbers now match the version numbers of DotNet Core
    // that ASP.Net Core is intended to work with. hence the jump to version 5.0 in 2020
    // Current version is the newly released 9.0 for both (11/2024) We're using 8.0  
}