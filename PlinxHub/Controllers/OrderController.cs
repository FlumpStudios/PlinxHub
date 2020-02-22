using Microsoft.AspNetCore.Mvc;
using vm = PlinxHub.API.Dtos;
using dm = PlinxHub.Common.Models.Orders;
using AutoMapper;
using PlinxHub.Service;
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

            //Make sure the logged in user is the same user as the user in the order
            if (!string.Equals(order.UserId, currentUser)) return StatusCode(403);

            return View(_mapper.Map<vm.Order>(order));
        }

        public async Task<ActionResult> YourOrders() =>
            View(_mapper.Map<IEnumerable<vm.Order>>(
                await _orderService.GetOrdersByUser(currentUser)));


        public ActionResult OrderConfirmation([FromRoute]int id, [FromQuery] bool updated)
        {
            if (updated)
            {
                ViewBag.ConfirmationMessage = $"Order {id} has now been successfully updated.";
            }
            else
            {
                ViewBag.ConfirmationMessage = $"Your order had been processed. Your order number number is {id}";
            }
            return View();
        }

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(vm.Order order)
        {
            try
            {
                order.UserId = currentUser;
                var response = await _orderService.GenerateNewOrder(_mapper.Map<dm.Order>(order));
                return RedirectToAction(nameof(OrderConfirmation), new { id = response.OrderNumber, updated = false });
            }
            catch(Exception e)
            {
                ViewBag.ErrorMessage = "Something went wrong processing your order :(";
                return RedirectToAction(nameof(Index));
            }
        }

      
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(vm.Order order)
        {
            try
            {
                if (order.OrderNumber < 0) return BadRequest();

                if (await _orderService.UpdateOrder(_mapper.Map<dm.Order>(order)))
                {
                    return RedirectToAction(nameof(OrderConfirmation), new { id = order.OrderNumber, updated = true });
                }

                return NotFound();
            }
            catch(Exception e)
            {
                ViewBag.ErrorMessage = "Something went wrong processing your order :(";
                return RedirectToAction(nameof(Index));
            }
        }

        public Guid currentUser
        {
            get => new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
        }
    }
}