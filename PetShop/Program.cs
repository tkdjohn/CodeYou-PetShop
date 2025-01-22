// See https://aka.ms/new-console-template for more information


using Data;
using Domain.Product;
using Domain.Product.Validators;
using DomainService;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Utilities;

var services = CreateServiceCollection();
var ProductService = services.GetService<IProductService>() ?? throw new Exception("Unable to locate a valid Product Logic module");

bool userIsDone = false;
while (!userIsDone)
{
    Console.WriteLine("Press 1 to add a product.");
    Console.WriteLine("Press 2 to view a product.");
    Console.WriteLine("Press 3 to view products that are in stock.");
    Console.WriteLine("Press 4 to view all products.");
    Console.WriteLine("Press 5 to view total retail value of inventory.");
    Console.WriteLine("Press 9 to add some test products.");
    Console.WriteLine("Type 'q' or 'exit' to quit.");
    
    // application will block here waiting for user to press <Enter>
    var userInput = UIUtilities.GetStringFromUser("===> ").ToLower() ?? "";

    switch (userInput) {
        case "exit":
        case "q":
            userIsDone = true;
            break;

        case "1":
            userInput = UIUtilities.GetStringFromUser("Do you wish to enter a <D>og Leash or <C>atFood? ").ToLower();
            switch (userInput[0]) {
                case 'd':
                    await AddProduct(GetProductFromUser<DogLeash>()).ConfigureAwait(false);
                    break;
                case 'c':
                    await AddProduct(GetProductFromUser<CatFood>()).ConfigureAwait(false);
                    break;
                default:
                    Console.WriteLine("InvalidChoice. Aborting.");
                    break;
            }
            break;

        case "2":
            var name = UIUtilities.GetStringFromUser("Enter the product name you want to view. ");
            var product = await ProductService.GetProductAsync(name).ConfigureAwait(false);
            if (product != null) {
                Console.WriteLine(product.Serialize());
                Console.WriteLine();
            } else {
                Console.WriteLine($"The product '{name}' was not found.\n");
            }
            break;
        case "3":
            var inStock = await ProductService.GetInStockProductsAsync().ConfigureAwait(false);
            Console.WriteLine("The following products are in stock: ");
            inStock.ForEach(p => Console.WriteLine(p.Serialize()));
            break;
        case "4":
            var products = await ProductService.GetProductsAsync().ConfigureAwait(false);
            products.ForEach(p => Console.WriteLine(p.Serialize()));
            break;
        case "5":
            var total = await ProductService.GetTotalPriceOfInventoryAsync().ConfigureAwait(false);
            Console.WriteLine($"The total inventory retail value is: {total}\n");
            break;
        case "9": //secret case to add some test products
            await AddTestProducts().ConfigureAwait(false);
            break;
    }
    Console.WriteLine("===============================");
}
async Task AddTestProducts() {
    Console.WriteLine("Adding test products.");
    await AddProduct(new DogLeash { Name = "Test Product 1", Quantity = 10, Price = 1.99M, LengthInches = 1 }).ConfigureAwait(false);
    await AddProduct(new CatFood { Name = "Out of Stock", Quantity = 0, Price = 15.99M, WeightPounds = 1 }).ConfigureAwait(false);
    await AddProduct(new DogLeash { Name = "Only 1 left", Quantity = 1, Price = 99.99M, LengthInches = 1 }).ConfigureAwait(false);

}

async Task AddProduct<T>(T? newProduct) where T: Product {
    if (newProduct != null) {
  
        var validationResult = newProduct.Validate<T>();
        if (validationResult.IsValid) {
            await ProductService.AddProductAsync<T>(newProduct);
            Console.WriteLine($"Added {newProduct.Name}.");
        } else {
            Console.WriteLine("Invalid Product data.");
            foreach (var error in validationResult.Errors) {
                Console.WriteLine(error);
            }
        }
    } else {
        Console.WriteLine("Invalid JSON. Nothing added.");
    }
}

//TODO: *jws* could probably move this to UIUtilities
static T? GetProductFromUser<T>() where T : Product {
    var json = UIUtilities.GetStringFromUser("Enter product JSON: ");
    return JsonSerializer.Deserialize<T>(json);
}

static IServiceProvider CreateServiceCollection() {
    var servicecollection = new ServiceCollection()
        .AddDbContext<IPetShopDbContext, PetShopDbContext>()
        .AddSingleton<IProductRepository, ProductRepository>()
        .AddSingleton<IProductService, ProductService>()
        // no need to inject validators - we're not consuming them that way.
        //.AddScoped<IValidator<IProduct>, ProductValidator>()
        //.AddScoped<IValidator<DogLeash>, DogLeashValidator>()
        //.AddScoped<IValidator<CatFood>, CatFoodValidator>()
        .AddTransient<ILogger, SimpleLogger>();

    return servicecollection.BuildServiceProvider();
}
