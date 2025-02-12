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
        IMapper mapper)
    : ControllerBase {
        private readonly ILogger<ProductsController> logger = logger;
        private readonly IOrderService orderService = orderService;
        private readonly IMapper mapper = mapper;

        [HttpGet]
        [Route("{orderId}")]
        public IActionResult GetOrders(int orderId) {
            try {
                return Ok(new { orderId });
            } catch (Exception ex) {
                //TODO: *jws* in class
                logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, $"Caught an exception: {ex}");
            }
        }
    }
}
