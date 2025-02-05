// See https://aka.ms/new-console-template for more information


using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetShop.Data;
using PetShop.DomainEntities;
using PetShop.DomainEntities.Validators;
using PetShop.DomainService;
using PetShop.Utilities;

var services = CreateServiceCollection();
var ProductService = services.GetService<IProductService>() ?? throw new Exception("Unable to locate a valid Product Logic module");
var OrderService = services.GetService<IOrderService>() ?? throw new Exception("Unable to locate a valid Order Logic module");
bool userIsDone = false;
while (!userIsDone) {
    Console.WriteLine("Type '1' to add/update a product.");
    Console.WriteLine("Type '2' to view a product.");
    Console.WriteLine("Type '3' to view products that are in stock.");
    Console.WriteLine("Type '4' to view all products.");

    Console.WriteLine("Type '6' to add/update an order.");
    Console.WriteLine("Type '7' to view an order.");
    Console.WriteLine("Type '8' to view all orders.");
    Console.WriteLine("Type 'a' to add some test data.");
    Console.WriteLine("Type 'x' to delete all data.");
    Console.WriteLine("Type 'q' to quit.");

    // application will block here waiting for user to press <Enter>
    var userInput = CLIUtilities.GetStringFromUser("===> ").ToLower() ?? "";

    switch (userInput) {
        case "exit":
        case "q":
            userIsDone = true;
            break;
        case "1":
            await AddUpdateEntity(GetEntityFromUser<Product>()).ConfigureAwait(false);
            break;
        case "2":
            await ViewProduct().ConfigureAwait(false);
            break;
        case "3":
            await ViewInStockProducts().ConfigureAwait(false);
            break;
        case "4":
            await ViewAllProduct().ConfigureAwait(false);
            break;
        case "5":

            break;
        case "6":
            await AddUpdateEntity(GetEntityFromUser<Order>()).ConfigureAwait(false);
            break;
        case "7":
            await ViewOrder().ConfigureAwait(false);
            break;
        case "8":
            await ViewallOrders().ConfigureAwait(false);
            break;
        case "9":

            break;
        case "a": 
            await AddTestData(ProductService, OrderService).ConfigureAwait(false);
            break;
        case "x":
            await DeleteAllData(ProductService, OrderService).ConfigureAwait(false);
            break;
    }
    Console.WriteLine("\n===============================\n");
}
static IServiceProvider CreateServiceCollection() {
    var servicecollection = new ServiceCollection()
        .AddDbContext<IPetShopDbContext, DatabaseContext>()
        .AddSingleton<IProductRepository, ProductRepository>()
        .AddSingleton<IOrderRepository, OrderRepository>()
        .AddSingleton<IProductService, ProductService>()
        .AddSingleton<IOrderService, OrderService>()
        .AddTransient<ILogger, SimpleLogger>();

    return servicecollection.BuildServiceProvider();
}

async Task AddTestData(IProductService productService, IOrderService orderService) {
    Console.WriteLine("Adding/Resetting test data.");
    await AddUpdateEntity(new Product {ProductId = 1, Name = "Super Short Leash", Quantity = 10, Price = 1.99M }).ConfigureAwait(false);
    await AddUpdateEntity(new Product { ProductId = 2, Name = "Dry Cat Food", Quantity = 0, Price = 15.99M }).ConfigureAwait(false);
    await AddUpdateEntity(new Product { ProductId = 100, Name = "Designer Leash", Quantity = 1, Price = 99.99M }).ConfigureAwait(false);
    await AddUpdateEntity(new Order { OrderId = 1, OrderDate = DateTime.Now, OrderProducts = { new OrderProduct { ProductId = 100, OrderQuantity = 5, UnitPrice = 99.99M } } }).ConfigureAwait(false);
    await AddUpdateEntity(new Order { OrderId = 2, OrderDate = DateTime.Now, OrderProducts = { new OrderProduct { ProductId = 2, OrderQuantity = 3, UnitPrice = 15.99M } } }).ConfigureAwait(false);
}
static async Task DeleteAllData(IProductService productService, IOrderService orderService) {
    var products = await productService.GetProductsAsync().ConfigureAwait(false);
    products.ForEach(async p => await productService.RemoveProductAsync(p));
    
    var orders = await orderService.GetOrdersAsync().ConfigureAwait(false); 
    orders.ForEach(async o => await orderService.RemoveOrderAsync(o).ConfigureAwait(false));
}

static T? GetEntityFromUser<T>() where T : EntityBase {
    var json = CLIUtilities.GetStringFromUser($"Enter {typeof(T)} JSON: ");
    T? entity = json.Deserialize<T>();
    if (entity == null) {
        Console.WriteLine("Invalid JSON.");
        return null;
    }
    return entity;
}

static bool ValidateEntity<T>(T entity) where T : EntityBase {
    var validationResult = entity.Validate<T>();
    if (!validationResult.IsValid) {
        Console.WriteLine("Invalid Product data.");
        foreach (var error in validationResult.Errors) {
            Console.WriteLine(error);
        }
        return false;
    }
    return true;
}


async Task AddEntity<T>(T? newEntity) where T : EntityBase {
    if (newEntity == null) {
        Console.WriteLine("Nothing added.");
        return;
    }
    if (!ValidateEntity<T>(newEntity)) {
        return;
    }

    if (newEntity is Product newProduct) {
        await ProductService.AddProductAsync(newProduct);
        Console.WriteLine($"Added {newProduct.Name}.");
        return;
    }

    if (newEntity is Order newOrder) {
        await OrderService.AddOrderAsync(newOrder);
        Console.WriteLine($"Added order # {newOrder.OrderId}.");
    }
}

async Task ViewProduct() {
    var name = CLIUtilities.GetStringFromUser("Enter the product name you want to view: ");
    var product = await ProductService.GetProductAsync(name).ConfigureAwait(false);
    if (product == null) {
        Console.WriteLine($"The product '{name}' was not found.\n");
        return;
    }
    Console.WriteLine(product.Serialize());
    Console.WriteLine();
}

async Task ViewOrder() {
    var orderId = CLIUtilities.GetIntFromUser("Enter the order id you want to view: ");
    var order = await OrderService.GetOrderAsync(orderId).ConfigureAwait(false);
    if (order == null) {
        Console.WriteLine($"Order Id {orderId} was not found.\n");
        return;
    }
    Console.WriteLine(order.ToString());
    Console.WriteLine();
}


async Task ViewInStockProducts() {
    var inStock = await ProductService.GetInStockProductsAsync().ConfigureAwait(false);
    Console.WriteLine("The following products are in stock: ");
    inStock.ForEach(p => Console.WriteLine(p.Serialize()));
}

async Task ViewAllProduct() {
    var products = await ProductService.GetProductsAsync().ConfigureAwait(false);
    products.ForEach(p => Console.WriteLine(p.Serialize()));
}

async Task ViewallOrders() {
    var orders = await OrderService.GetOrdersAsync().ConfigureAwait(false);
    orders.ForEach(o => Console.WriteLine(o.ToString()));
}

async Task AddUpdateEntity<T>(T? entityUpdate) where T : EntityBase {

    if (entityUpdate == null) {
        Console.WriteLine("Nothing updated.");
        return;
    }
    
    if (!ValidateEntity(entityUpdate)) {
        return;
    }

    if (entityUpdate is Product product) {
        if (!await ProductService.ProductExists(product.ProductId)) {
            Console.WriteLine($"Product not found. Adding.");
            await AddEntity(product);
            return;
        }
        Console.WriteLine($"Updating product Id {product.ProductId}");
        await ProductService.UpdateProduct(product).ConfigureAwait(false);
        return;
    }
    if (entityUpdate is Order order) {
        //TODO: edit PS10 instructions to include valid json now that OrderProduct is a thing.
        if (!await OrderService.OrderExists(order.OrderId)) {
            Console.WriteLine($"Order not found. Adding.");
            await AddEntity(order);
            return;
        }
        Console.WriteLine($"Updating order Id {order.OrderId}");
        await OrderService.UpdateOrder(order).ConfigureAwait(false);
        return;
    }
}
