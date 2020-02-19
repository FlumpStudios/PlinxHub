using Microsoft.AspNetCore.Mvc;
using vm = PlinxHub.API.Dtos;
using AutoMapper;
using PlinxHub.Service;
using dm = PlinxHub.Common.Models.Orders;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System;

using System.Security.Claims;
using System.Collections.Generic;

namespace PlinxHub.API.Controllers
{
    [Authorize]
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
     
        public async Task<ActionResult> Index([FromQuery] int orderNumber)
        {
            if (orderNumber <= 0 ) return View();
            
            var order = await _orderService.GetOrder(orderNumber);

            return View(_mapper.Map<vm.Request.OrderRequest>(order));
        }

        
        public async Task<ActionResult> YourOrders()
        {
            return View(_mapper.Map<IEnumerable<vm.Response.OrderResponse>>(await _orderService.GetOrdersByUser(GetUser)));
        }

        public ActionResult OrderConfirmation([FromRoute]int id)
        {
            ViewBag.ConfirmationMessage = $"Your order had been processed. Your order number number is {id}";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(vm.Request.OrderRequest order)
        {
            try
            {
                order.UserId = GetUser;
                var response = await _orderService.GenerateNewOrder(_mapper.Map<dm.Order>(order));
                return RedirectToAction(nameof(OrderConfirmation), new { id = response.OrderNumber });
            }
            catch(Exception e)
            {
                ViewBag.ErrorMessage = "Something went wrong processing your order :(";
                return RedirectToAction(nameof(Index));
            }
        }

        public Guid GetUser
        {
            get => new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
        }
    }
}