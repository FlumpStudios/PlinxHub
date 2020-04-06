using System.Collections.Generic;
using vm = PlinxHub.API.Dtos;
using dm = PlinxHub.Common.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlinxHub.Service;
using System.Threading.Tasks;
using System;

namespace PlinxHub.API.Controllers
{
    /// <summary>
    /// Admin controller
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class AdministrationController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for the admin controller 
        /// </summary>
        /// <param name="orderService"></param>
        /// <param name="mapper"></param>
        public AdministrationController(
            IOrderService orderService,
            IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns the index page for the admin area
        /// </summary>
        [HttpGet]
        public async Task<ActionResult> Index(vm.OrderFilters filters)
        {
            var f = _mapper.Map<dm.Filters.OrderFilters>(filters);

            var response = new vm.OrderWithFilters
            {
                Order = _mapper.Map<IEnumerable<vm.Order>>(await _orderService.GetOrder(f)),
                Filters = filters
            };

            return View(response);
        }

        /// <summary>
        /// Get API key from order number
        /// </summary>
        /// <param name="orderId"></param>        
        [HttpGet]
        public async Task<ActionResult<string>> GetApiKey(Guid orderId) =>
             await _orderService.GetApiKeyByOrder(orderId);
    }
}