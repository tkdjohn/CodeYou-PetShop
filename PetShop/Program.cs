// See https://aka.ms/new-console-template for more information


using Data;
using DomainEntities;
using DomainEntities.Validators;
using DomainService;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Utilities;

var services = CreateServiceCollection();
var ProductService = services.GetService<IProductService>() ?? throw new Exception("Unable to locate a valid Product Logic module");
var OrderService = services.GetService<IOrderService>() ?? throw new Exception("Unable to locate a valid Order Logic module");
bool userIsDone = false;
while (!userIsDone) {
    Console.WriteLine("Press 1 to add a product.");
    Console.WriteLine("Press 2 to view a product.");
    Console.WriteLine("Press 3 to view products that are in stock.");
    Console.WriteLine("Press 4 to view all products.");
    Console.WriteLine("Press 5 to update a product by Id.");
    Console.WriteLine("Press 6 to add an order.");
    Console.WriteLine("Press 7 to view an order.");
    Console.WriteLine("Press 8 to view all orders.");
    Console.WriteLine("Press 9 to update an order by Id.");
    Console.WriteLine("Press a to add some test data.");
    Console.WriteLine("Type 'q' or 'exit' to quit.");

    // application will block here waiting for user to press <Enter>
    var userInput = UIUtilities.GetStringFromUser("===> ").ToLower() ?? "";

    switch (userInput) {
        case "exit":
        case "q":
            userIsDone = true;
            break;
        case "1":
            await EnterProduct(ProductService).ConfigureAwait(false);
            break;
        case "2":
            await ViewProduct(ProductService).ConfigureAwait(false);
            break;
        case "3":
            await ViewInStockProducts(ProductService).ConfigureAwait(false);
            return;
        case "4":
            await ViewAllProduct(ProductService).ConfigureAwait(false);
            break;
        case "5":
            await UpdateProduct(ProductService).ConfigureAwait(false);
            break;
        case "6":
            await EnterOrder(OrderService).ConfigureAwait(false);
            break;
        case "7":
            await ViewOrder(OrderService).ConfigureAwait(false);
            break;
        case "8":
            await ViewallOrders(OrderService).ConfigureAwait(false);
            break;
        case "9":
            await UpdateOrder(OrderService).ConfigureAwait(false);
            break;
        case "a": 
            await AddTestData(ProductService, OrderService).ConfigureAwait(false);
            break;
    }
    Console.WriteLine("\n===============================\n");
}

static async Task AddTestData(IProductService productService, IOrderService orderService) {
    Console.WriteLine("Adding test products.");
    await AddProduct(productService, new DogLeash { Name = "Test Product 1", Quantity = 10, Price = 1.99M, LengthInches = 1 }).ConfigureAwait(false);
    await AddProduct(productService, new CatFood { Name = "Out of Stock", Quantity = 0, Price = 15.99M, WeightPounds = 1 }).ConfigureAwait(false);
    await AddProduct(productService, new DogLeash { Name = "Only 1 left", Quantity = 1, Price = 99.99M, LengthInches = 1 }).ConfigureAwait(false);
    await AddOrder(orderService, new Order { OrderDate = DateTime.Now }).ConfigureAwait(false);
}
static async Task EnterProduct(IProductService productService) {
    string userInput = UIUtilities.GetStringFromUser("Do you wish to enter a <D>og Leash or <C>atFood? ").ToLower();
    switch (userInput[0]) {
        case 'd':
            await AddProduct(productService, GetEntity<DogLeash>()).ConfigureAwait(false);
            break;
        case 'c':
            await AddProduct(productService, GetEntity<CatFood>()).ConfigureAwait(false);
            break;
        default:
            Console.WriteLine("InvalidChoice. Aborting.");
            break;
    }
}

static async Task EnterOrder(IOrderService orderService) {
    var newOrder = GetEntity<Order>();
    if (newOrder == null) {
        Console.WriteLine("Invalid JSON. Nothing added.");
        return;
    }
    await AddOrder(orderService, newOrder);
}

static async Task AddProduct<T>(IProductService productService, T? newProduct) where T : Product {
    if (newProduct == null) {
        Console.WriteLine("Invalid JSON. Nothing added.");
        return;
    }

    var validationResult = newProduct.Validate<T>();
    if (!validationResult.IsValid) {
        Console.WriteLine("Invalid Product data.");
        foreach (var error in validationResult.Errors) {
            Console.WriteLine(error);
        }
        return;
    }
    await productService.AddProductAsync<T>(newProduct);
    Console.WriteLine($"Added {newProduct.Name}.");
}

static async Task AddOrder(IOrderService orderService, Order newOrder) {
    var validationResult = newOrder.Validate();
    if (!validationResult.IsValid) {
        Console.WriteLine("Invalid Product data.");
        foreach (var error in validationResult.Errors) {
            Console.WriteLine(error);
        }
        return;
    }
    await orderService.AddOrderAsync(newOrder);
    Console.WriteLine($"Added order # {newOrder.OrderId}.");
}
static T? GetEntity<T>() where T : BaseEntity {
    var json = UIUtilities.GetStringFromUser($"Enter {typeof(T)} JSON: ");
    return BaseEntity.Deserialize<T>(json);
}

static IServiceProvider CreateServiceCollection() {
    var servicecollection = new ServiceCollection()
        .AddDbContext<IPetShopDbContext, PetShopDbContext>()
        .AddSingleton<IProductRepository, ProductRepository>()
        .AddSingleton<IOrderRepository, OrderRepository>()
        .AddSingleton<IProductService, ProductService>()
        .AddSingleton<IOrderService, OrderService>()
        .AddTransient<ILogger, SimpleLogger>();

    return servicecollection.BuildServiceProvider();
}

static async Task ViewProduct(IProductService productService) {
    var name = UIUtilities.GetStringFromUser("Enter the product name you want to view. ");
    var product = await productService.GetProductAsync(name).ConfigureAwait(false);
    if (product == null) {
        Console.WriteLine($"The product '{name}' was not found.\n");
        return;
    }
    Console.WriteLine(product.Serialize());
    Console.WriteLine();
}

static async Task ViewOrder(IOrderService orderService) {
    var orderId = UIUtilities.GetIntFromUser("Enter the order id you want to view. ");
    var order = await orderService.GetOrderAsync(orderId).ConfigureAwait(false);
    if (order == null) {
        Console.WriteLine($"Order Id {orderId} was not found.\n");
        return;
    }
    Console.WriteLine(order.ToString());
    Console.WriteLine();
}


static async Task ViewInStockProducts(IProductService productService) {
    var inStock = await productService.GetInStockProductsAsync().ConfigureAwait(false);
    Console.WriteLine("The following products are in stock: ");
    inStock.ForEach(p => Console.WriteLine(p.Serialize()));
}

static async Task ViewAllProduct(IProductService productService) {
    var products = await productService.GetProductsAsync().ConfigureAwait(false);
    products.ForEach(p => Console.WriteLine(p.Serialize()));
}
static async Task ViewallOrders(IOrderService orderService) {
    var orders = await orderService.GetOrdersAsync().ConfigureAwait(false);
    orders.ForEach(o => Console.WriteLine(o.ToString()));
}
static async Task UpdateProduct(IProductService productService) {
    var productUpdate = GetEntity<Product>();
    if (productUpdate != null) {

        if (!await productService.ProductExists(productUpdate.ProductId)) {
            Console.WriteLine($"Product not found. Adding.");
            await AddProduct(productService, productUpdate);
            return;
        }
        Console.WriteLine($"Updating product Id {productUpdate.ProductId}");
        //TODO: abstract validate code and call it here.
        await productService.UpdateProduct(productUpdate).ConfigureAwait(false);
    }
}
static async Task UpdateOrder(IOrderService orderService) {
    var orderUpdate = GetEntity<Order>();
    if (orderUpdate == null) {
        return;
    }

    if (!await orderService.OrderExists(orderUpdate.OrderId)) {
        Console.WriteLine($"Order not found. Adding.");
        await AddOrder(orderService, orderUpdate);
        return;
    }
    Console.WriteLine($"Updating order Id {orderUpdate.OrderId}");
    //TODO: abstract validate code and call it here.
    await orderService.UpdateOrder(orderUpdate).ConfigureAwait(false);
}

