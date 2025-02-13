using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PetShop.DomainService;

namespace PetShop.WebApi.Controllers {
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class OrdersController (
        ILogger < ProductsController > logger,
        IOrderService orderService,
        IMapper mapper,
        LinkGenerator linkGenerator)
    : ControllerBase {
        private readonly ILogger<ProductsController> logger = logger;
        private readonly IOrderService orderService = orderService;
        private readonly IMapper mapper = mapper;
        private readonly LinkGenerator linkGenerator = linkGenerator;

        [HttpGet]
        [Route("{orderId}")]
        public IActionResult GetOrder(int orderId) {
            try {
                //TODO: fill out this method
                return Ok(new { orderId });
            } catch (Exception ex) {
                logger.LogError(ex,"Caught exception Getting an Order.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Caught an exception: {ex}");
            }
        }
    }
}
