// See https://aka.ms/new-console-template for more information


using Domain.Product;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Utilities;

var services = CreateServiceCollection();
var MyProductLogic = services.GetService<IProductLogic>();

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
            var leash = MyProductLogic.GetDogLeash(name);
            if (leash != null) {
                Console.WriteLine(MyProductLogic.GetDogLeash(name));
            } else {
                Console.WriteLine($"The product '{name}' was not found.");
            }
            break;
        case "3":
            var inStock = MyProductLogic.GetOnlyInStockProducts();
            Console.WriteLine("The following products are in stock: ");
            inStock.ForEach(p => Console.WriteLine(p));
            break;
        case "4":
            Console.WriteLine($"The total inventory retail value is: {MyProductLogic.GetTotalPriceOfInventory()}");
            break;
    }
    Console.WriteLine("===============================");
} 

static DogLeash GetDogLeashFromUser()
{
    var newLeash = new DogLeash();
    Console.WriteLine("Adding a Dog Leash.\n");
    newLeash.Name = UIUtilities.GetStringFromUser("Enter a Name: ");
    newLeash.Description = UIUtilities.GetStringFromUser("Enter a Description: ");

    newLeash.Qty = UIUtilities.GetIntFromUser("Enter the quantity: ");

    newLeash.Price = UIUtilities.GetDecimalFromUser("Enter the price: ");

    newLeash.LengthInches = UIUtilities.GetIntFromUser("Enter the Length: ");

    newLeash.Material = UIUtilities.GetStringFromUser("Enter the material: ");

    return newLeash;
}


IServiceProvider CreateServiceCollection () {
    var servicecollection = new ServiceCollection()
        .AddSingleton<IProductLogic, ProductLogic>()
        .AddTransient<ILogger, SimpleLogger>();

    return servicecollection.BuildServiceProvider();
}