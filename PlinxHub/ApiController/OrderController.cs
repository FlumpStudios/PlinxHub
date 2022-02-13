using AutoMapper;
using vm = PlinxHub.API.Dtos;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlinxHub.Service;

namespace PlinxHub.API.ApiController
{
    /// <summary>
    /// Order api controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        const string HEADER_KEY = "api_key";

        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Order api controller constructor method
        /// </summary>
        /// <param name="orderService"></param>
        /// <param name="mapper"></param>
        public OrderController(
            IOrderService orderService,
            IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get order details from API key
        /// </summary>
        [HttpGet("GetOrder")]
        public async Task<ActionResult<vm.Order>> GetOrder()
        {
            var key =  GetApiKey;
            if (string.IsNullOrEmpty(key)) return Unauthorized();

            var order = await _orderService.GetOrderByApiKey(key);

            if (order == null) return Unauthorized();

            return _mapper.Map<vm.Order>(order);
        }

        private string GetApiKey => Request.Headers[HEADER_KEY];
    }
}
