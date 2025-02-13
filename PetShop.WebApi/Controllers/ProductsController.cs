using Microsoft.AspNetCore.Mvc;
using PetShop.DomainEntities.Validators;
using PetShop.DomainService;
using PetShop.WebApi.Mappers;
using PetShop.WebApi.Requests;
using PetShop.WebApi.Responses;

namespace PetShop.WebApi.Controllers {
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProductsController(
        ILogger<ProductsController> logger, 
        IProductService productService,
        LinkGenerator linkGenerator
    ) : ControllerBase {
        private readonly ILogger<ProductsController> logger = logger;
        private readonly IProductService productService = productService;
        private readonly LinkGenerator linkGenerator = linkGenerator;

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<ProductResponseModel[]>> GetProducts(bool InStockOnly = false) {
            try {
                var productEntities = await productService.GetProductsAsync(InStockOnly).ConfigureAwait(false);

                return Ok(productEntities.ToProductResponseModelArray());
            } catch (Exception ex) { 
                logger.LogError(ex, "Caught an exception Getting Products");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Caught an exception: {ex}");
            }
        }

        [HttpGet]
        [Route("{productId}")]
        public async Task<ActionResult<ProductResponseModel>> GetProduct(int productId) {
            try {
                var productEntity = await productService.GetProductAsync(productId).ConfigureAwait(false);
                if (productEntity == null) {
                    return NotFound();
                }
                return Ok(productEntity.ToProductResponseModel());
            } catch (Exception ex) {
                logger.LogError(ex, "Caught an exception Getting a Product");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Caught an exception: {ex}");
            }
        }

        [HttpDelete]
        [Route("{productId}")]
        public async Task<ActionResult> DeleteProduct(int productId) {
            try {
                var productToDelete = await productService.GetProductAsync(productId).ConfigureAwait(false);
                if (productToDelete == null) {
                    return NotFound();
                }
                await productService.RemoveProductAsync(productToDelete).ConfigureAwait(false);

                return NoContent();
            } catch (Exception ex) {
                logger.LogError(ex, "Caught an exception Deleting a Product.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Caught an exception: {ex}");
            }
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<ProductResponseModel>> AddProduct(ProductRequestModel request) {
            try {
                var productToAdd = request.ToProduct();
                var validationResult = productToAdd.Validate();
                if (!validationResult.IsValid) {
                    return BadRequest(validationResult.Errors);
                }

                var product = await productService.AddProductAsync(productToAdd).ConfigureAwait(false);
                var newProductUrl = linkGenerator.GetPathByAction(
                    "Get",
                    nameof(ProductsController).Replace("Controller",""),
                    new { productId = product.ProductId });
                return Created(newProductUrl, product.ToProductResponseModel());
            } catch (Exception ex) {
                logger.LogError(ex, "Caught an exception Adding a Product.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Caught an exception: {ex}");
            }
        }

        [HttpPut]
        [Route("{ProductId}")]
        public async Task<ActionResult<ProductResponseModel>> UpdateProduct(
            int productId, 
            ProductRequestModel request
        ) {
            try {
                //TODO: fill out this method
                return NoContent();
            } catch (Exception ex) {
                logger.LogError(ex, "Caught exception Updating a Product.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Caught an exception: {ex}");
            }
        }
    }
}
