using Microsoft.AspNetCore.Mvc;
using PlinxHub.API.Dtos.Response;
using AutoMapper;
using PlinxHub.Service;
using dm = PlinxHub.Common.Models.Orders;
using System.Threading.Tasks;

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

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OrderConfirmation(int id)
        {
            ViewBag.ConfirmationMessage = $"Your order had been processed. Your order number number is {id}";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Order order)
        {
            try
            {
                await _orderService.GenerateNewOrder(_mapper.Map<dm.Order>(order));
                return RedirectToAction(nameof(OrderConfirmation));
            }
            catch
            {
                ViewBag.ErrorMessage = "Something went wrong processing your order :(";
                return View();
            }
        }
    }
}