// See https://aka.ms/new-console-template for more information


using Domain.Product;
using Domain.Product.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Utilities;

var services = CreateServiceCollection();
var MyProductLogic = services.GetService<IProductLogic>() ?? throw new Exception("Unable to locate a valid Product Logic module");

bool userIsDone = false;
while (!userIsDone)
{
    Console.WriteLine("Press 1 to add a product.");
    Console.WriteLine("Press 2 to view a product.");
    Console.WriteLine("Press 3 to view products that are in stock.");
    Console.WriteLine("Press 4 to view all products.");
    Console.WriteLine("Press 5 to view total retail value of inventory.");
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
                    AddProduct(GetProductFromUser<DogLeash>());
                    break;
                case 'c':
                    AddProduct(GetProductFromUser<CatFood>());
                    break;
                default:
                    Console.WriteLine("InvalidChoice. Aborting.");
                    break;
            }
            break;

        case "2":
            var name = UIUtilities.GetStringFromUser("Enter the product name you want to view. ");
            var product = MyProductLogic.GetProduct(name);
            if (product != null) {
                Console.WriteLine(product);
                Console.WriteLine();
            } else {
                Console.WriteLine($"The product '{name}' was not found.\n");
            }
            break;
        case "3":
            var inStock = MyProductLogic.GetOnlyInStockProducts();
            Console.WriteLine("The following products are in stock: ");
            inStock.ToList().ForEach(p => Console.WriteLine(p));
            break;
        case "4":
            var products = MyProductLogic.GetProducts();
            products.ToList().ForEach(p => Console.WriteLine(p.Serialize()));
            break;
        case "5":
            Console.WriteLine($"The total inventory retail value is: {MyProductLogic.GetTotalPriceOfInventory()}\n");
            break;
        case "9": //secret case to add some test products
            AddTestProducts();
            break;
    }
    Console.WriteLine("===============================");
}
void AddTestProducts() {
    Console.WriteLine("Adding test products.");
    AddProduct(new DogLeash { Name = "Test Product 1", Quantity = 10, Price = 1.99M, LengthInches = 1 });
    AddProduct(new CatFood { Name = "Out of Stock", Quantity = 0, Price = 15.99M, WeightPounds = 1 });
    AddProduct(new DogLeash { Name = "Only 1 left", Quantity = 1, Price = 99.99M, LengthInches = 1 });

}

void AddProduct<T>(T? newProduct) where T: IProduct{
    if (newProduct != null) {
  
        var validationResult = newProduct.Validate<T>();
        if (validationResult.IsValid) {
            MyProductLogic.AddProduct<T>(newProduct);
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
static T? GetProductFromUser<T>() where T : IProduct {
    var json = UIUtilities.GetStringFromUser("Enter product JSON: ");
    return JsonSerializer.Deserialize<T>(json);
}

static IServiceProvider CreateServiceCollection() {
    var servicecollection = new ServiceCollection()
        .AddSingleton<IProductLogic, ProductLogic>()
        // no need to inject validators - we're not consuming them that way.
        //.AddScoped<IValidator<IProduct>, ProductValidator>()
        //.AddScoped<IValidator<DogLeash>, DogLeashValidator>()
        //.AddScoped<IValidator<CatFood>, CatFoodValidator>()
        .AddTransient<ILogger, SimpleLogger>();

    return servicecollection.BuildServiceProvider();
}
