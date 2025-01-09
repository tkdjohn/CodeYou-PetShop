// See https://aka.ms/new-console-template for more information


using Domain.Product;
using Domain.Product.Validators;
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
    Console.WriteLine("Press 4 to view total retail value of inventory.");
    Console.WriteLine("Type 'q' or 'exit' to quit.");
    // application will block here waiting for user to press <Enter>
    var userInput = Console.ReadLine()?.ToLower() ?? "";

    switch (userInput)
    {
        case "exit":
        case "q":
            userIsDone = true;
            break;

        case "1":
            MyProductLogic.AddProduct( GetDogLeashFromUser());
            Console.WriteLine("Dog leash added.");
            break;

        case "2":
            var name = UIUtilities.GetStringFromUser("Enter the product name you want to view. ");
            var leash = MyProductLogic.GetProduct<DogLeash>(name);
            if (leash != null) {
                Console.WriteLine(leash);
            } else {
                Console.WriteLine($"The product '{name}' was not found.");
            }
            break;
        case "3":
            var inStock = MyProductLogic.GetOnlyInStockProducts();
            Console.WriteLine("The following products are in stock: ");
            inStock.ToList().ForEach(p => Console.WriteLine(p));
            break;
        case "4":
            Console.WriteLine($"The total inventory retail value is: {MyProductLogic.GetTotalPriceOfInventory()}");
            break;
    }
    Console.WriteLine("===============================");
} 

static DogLeash GetDogLeashFromUser()
{
    Console.WriteLine("Adding a Dog Leash.\n");
    var json = UIUtilities.GetStringFromUser("Enter Dog Leash Json: ");
    return JsonSerializer.Deserialize<DogLeash>(json) ?? new DogLeash();
}


IServiceProvider CreateServiceCollection () {
    var servicecollection = new ServiceCollection()
        .AddSingleton<IProductLogic, ProductLogic>()
        .AddTransient<DogLeashValidator>()
        .AddTransient<ILogger, SimpleLogger>();

    return servicecollection.BuildServiceProvider();
}