﻿// See https://aka.ms/new-console-template for more information


using PetShop.Product;
using Utilities;

IProductLogic ProductLogic = new ProductLogic();

bool userIsDone = false;
while (!userIsDone)
{
    Console.WriteLine("Press 1 to add a product.");
    Console.WriteLine("Press 2 to view a product.");
    Console.WriteLine("Press 3 to view products that are in stock.");
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
            ProductLogic.AddProduct( GetDogLeashFromUser());
            Console.WriteLine("Dog leash added.");
            break;

        case "2":
            var name = UIUtils.GetStringFromUser("Enter the product name you want to view. ");
            var leash = ProductLogic.GetDogLeash(name);
            if (leash != null) {
                Console.WriteLine(ProductLogic.GetDogLeash(name));
            } else {
                Console.WriteLine($"The product '{name}' was not found.");
            }
            break;
        case "3":
            var inStock = ProductLogic.GetOnlyInStockProducts();
            Console.WriteLine("The following products are in stock: ");
            inStock.ForEach(p => Console.WriteLine(p));
            break;
    }
    Console.WriteLine("===============================");
} 

static DogLeash GetDogLeashFromUser()
{
    var newLeash = new DogLeash();
    Console.WriteLine("Adding a Dog Leash.\n");
    newLeash.Name = UIUtils.GetStringFromUser("Enter a Name: ");
    newLeash.Description = UIUtils.GetStringFromUser("Enter a Description: ");

    newLeash.Qty = UIUtils.GetIntFromUser("Enter the quantity: ");

    newLeash.Price = UIUtils.GetDecimalFromUser("Enter the price: ");

    newLeash.LengthInches = UIUtils.GetIntFromUser("Enter the Length: ");

    newLeash.Material = UIUtils.GetStringFromUser("Enter the material: ");

    return newLeash;
}


