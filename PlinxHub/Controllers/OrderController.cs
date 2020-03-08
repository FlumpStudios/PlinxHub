using Microsoft.AspNetCore.Mvc;
using vm = PlinxHub.API.Dtos;
using dm = PlinxHub.Common.Models;
using AutoMapper;
using PlinxHub.Service;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Security.Claims;
using System.Collections.Generic;
using PlinxHub.Common.Extensions;

namespace PlinxHub.API.Controllers
{
    /// <summary>
    /// Controller for customer orders
    /// </summary>
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

    /// <summary>
    /// Constructor for the order controller 
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
        /// Index view action for orders
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public async Task<ActionResult> Index([FromQuery] Guid orderNumber)
        {
            if (orderNumber == Guid.Empty ) return View();

            var order = await _orderService.GetOrder(orderNumber);

            //Make sure the logged in user is the same user as the user in the order
            if (!CanAccessRecords(order.UserId)) return StatusCode(403);

            return View(_mapper.Map<vm.Order>(order));
        }

        /// <summary>
        /// View action for the list of users orders
        /// </summary>
        public async Task<ActionResult> YourOrders(vm.OrderFilters filters)
        {
            var f = _mapper.Map<dm.Filters.OrderFilters>(filters);

            var response = new vm.OrderWithFilters
            {
                Order = _mapper.Map<IEnumerable<vm.Order>>(await _orderService.GetOrdersByUser(CurrentUser,f)),
                Filters = filters
            };

            return View(response);
        }


        /// <summary>
        /// View action for the orders list view
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updated"></param>    
        public ActionResult OrderConfirmation([FromRoute]string id, [FromQuery] bool updated)
        {
            if (updated)
            {
                ViewBag.ConfirmationMessage = $"Order {id} has now been successfully updated.";
            }
            else
            {
                ViewBag.ConfirmationMessage = $"Your order had been processed. Your order number reference is {id}";
            }
            return View();
        }

        /// <summary>
        /// Post action to create a new order
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(vm.Order order)
        {
            try
            {
                order.UserId = CurrentUser;
                var response = await _orderService.GenerateNewOrder(_mapper.Map<dm.Orders.Order>(order));
                return RedirectToAction(nameof(OrderConfirmation), new { id = response.OrderNumber.GetSubString(), updated = false });
            }
            catch(Exception e)
            {
                ViewBag.ErrorMessage = "Something went wrong processing your order :(";
                return RedirectToAction(nameof(Index));
            }
        }
      
        /// <summary>
        /// Post action to update a current record
        /// </summary>
        /// <param name="order"></param> 
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(vm.Order order)
        {
            try
            {
                if (order.OrderNumber == Guid.Empty) return BadRequest();
                
                //Make sure the logged in user is the same user as the user in the order
                if (!CanAccessRecords(order.UserId)) return StatusCode(403);

                if (await _orderService.UpdateOrder(_mapper.Map<dm.Orders.Order>(order)))
                {
                    return RedirectToAction(nameof(OrderConfirmation), new { id = order.OrderNumber.GetSubString(), updated = true });
                }

                return NotFound();
            }
            catch
            {
                ViewBag.ErrorMessage = "Something went wrong processing your order :(";
                return RedirectToAction(nameof(Index));
            }
        }

        /// <summary>
        /// Check if user is allowed to access records
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private bool CanAccessRecords(Guid userId) =>   
            Equals(userId, CurrentUser) || IsAdmin;


        /// <summary>
        /// Private getter to get the current logged in user
        /// </summary>     
        private Guid CurrentUser => new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));

        /// <summary>
        /// Check if user has admin rights
        /// </summary>
        private bool IsAdmin => User.IsInRole("Admin");
    }
}