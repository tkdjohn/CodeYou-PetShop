// See https://aka.ms/new-console-template for more information


using PetShop.Product;
using Utilities;

var ProductLogic = new ProductLogic();

bool userIsDone = false;
while (!userIsDone)
{
    Console.WriteLine("Press 1 to add a product.");
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
    }
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

    //Console.WriteLine(newLeash.ToString());
    return newLeash;
}


