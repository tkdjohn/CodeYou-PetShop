using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetShop.DomainEntities;
using PetShop.DomainService;
using PetShop.DomainEntities.Validators;

namespace PetShop.WebApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class TestDataController(
        ILogger<TestDataController> logger,
        IProductService productService,
        IOrderService orderService
    ) : ControllerBase {
        private readonly ILogger<TestDataController> logger = logger;
        private readonly IProductService productService = productService;
        private readonly IOrderService orderService = orderService;

        [HttpOptions(nameof(ClearData))]
        public async Task<IActionResult> ClearData() {
            try {
                var products = await productService.GetProductsAsync().ConfigureAwait(false);
                products.ForEach(async p => await productService.RemoveProductAsync(p));

                var orders = await orderService.GetOrdersAsync().ConfigureAwait(false);
                orders.ForEach(async o => await orderService.RemoveOrderAsync(o).ConfigureAwait(false));

                return Ok();
            } catch (Exception ex) {
                logger.LogError(ex, "Caught an exception Getting Products");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Caught an exception: {ex}");
            }
        }

        [HttpOptions(nameof(AddTestData))]
        public async Task<IActionResult> AddTestData() {
            try {
                await AddUpdateEntity(new Product { ProductId = 1, Name = "Super Short Leash", Quantity = 10, Price = 1.99M }).ConfigureAwait(false);
                await AddUpdateEntity(new Product { ProductId = 2, Name = "Dry Cat Food", Quantity = 0, Price = 15.99M }).ConfigureAwait(false);
                await AddUpdateEntity(new Product { ProductId = 100, Name = "Designer Leash", Quantity = 1, Price = 99.99M }).ConfigureAwait(false);
                await AddUpdateEntity(new Order { OrderId = 1, OrderDate = DateTime.Now, OrderProducts = { new OrderProduct { ProductId = 100, OrderQuantity = 5, UnitPrice = 99.99M } } }).ConfigureAwait(false);
                await AddUpdateEntity(new Order { OrderId = 2, OrderDate = DateTime.Now, OrderProducts = { new OrderProduct { ProductId = 2, OrderQuantity = 3, UnitPrice = 15.99M } } }).ConfigureAwait(false);
                return Ok();
            } catch (Exception ex) {
                logger.LogError(ex, "Caught an exception Getting Products");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Caught an exception: {ex}");
            }
        }


        //TODO: *jws* these methods are duplicated from CLI;s Program.cs
        // find a common place to store these utilities. (DomainEntities Maybe?)
        // TODO: *jws* also have the other controllers utilize them maybe
        private async Task AddUpdateEntity<T>(T? entityUpdate) where T : EntityBase {

            if (entityUpdate == null) {
                logger.LogInformation("Nothing updated.");
                return;
            }

            if (!ValidateEntity(entityUpdate)) {
                return;
            }

            if (entityUpdate is Product product) {
                if (!await productService.ProductExists(product.ProductId)) {
                    logger.LogInformation($"Product not found. Adding.");
                    await AddEntity(product);
                    return;
                }
                logger.LogInformation("Updating product Id {ProductId}", product.ProductId);
                await productService.UpdateProduct(product).ConfigureAwait(false);
                return;
            }
            if (entityUpdate is Order order) {
                //TODO: edit PS10 instructions to include valid json now that OrderProduct is a thing.
                if (!await orderService.OrderExists(order.OrderId)) {
                    logger.LogInformation($"Order not found. Adding.");
                    await AddEntity(order);
                    return;
                }
                logger.LogInformation("Updating order Id {OrderId}", order.OrderId);
                await orderService.UpdateOrder(order).ConfigureAwait(false);
                return;
            }
        }
        private bool ValidateEntity<T>(T entity) where T : EntityBase {
            var validationResult = entity.Validate<T>();
            if (!validationResult.IsValid) {
                logger.LogInformation("Invalid Product data.");
                foreach (var error in validationResult.Errors) {
                    logger.LogError("Validation Error: {Error}", error.ToString());
                }
                return false;
            }
            return true;
        }

        private async Task AddEntity<T>(T? newEntity) where T : EntityBase {
            if (newEntity == null) {
                logger.LogInformation("Nothing added.");
                return;
            }
            if (!ValidateEntity<T>(newEntity)) {
                return;
            }

            if (newEntity is Product newProduct) {
                await productService.AddProductAsync(newProduct);
                logger.LogInformation("Added {Name}.", newProduct.Name);
                return;
            }

            if (newEntity is Order newOrder) {
                await orderService.AddOrderAsync(newOrder);
                logger.LogInformation("Added order # {OrderId}.", newOrder.OrderId);
            }
        }
    }
}