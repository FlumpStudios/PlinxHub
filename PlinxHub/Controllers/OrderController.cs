using Microsoft.AspNetCore.Mvc;
using PlinxHub.API.Dtos.Response;
using AutoMapper;
using PlinxHub.Service;
using dm = PlinxHub.Common.Models.Orders;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
namespace PlinxHub.API.Controllers
{
    public class OrderController : Controller
    {        
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(
            IOrderService orderService,
            IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OrderConfirmation([FromRoute]int id)
        {
            ViewBag.ConfirmationMessage = $"Your order had been processed. Your order number number is {id}";
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Order order)
        {
            try
            {                
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                order.UserId = new System.Guid(userId);
                var response = await _orderService.GenerateNewOrder(_mapper.Map<dm.Order>(order));
                return RedirectToAction(nameof(OrderConfirmation), new { id = response.OrderNumber });
            }
            catch
            {
                ViewBag.ErrorMessage = "Something went wrong processing your order :(";
                return View();
            }
        }
    }
}