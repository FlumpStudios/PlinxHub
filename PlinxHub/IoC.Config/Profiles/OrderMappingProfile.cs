using AutoMapper;
using vm = PlinxHub.API.Dtos;
using dm = PlinxHub.Common.Models;

namespace PlinxHub.Ioc.Config.Profiles
{
    /// <summary>
    /// Automapper order mapping profile
    /// </summary>
    public class OrderMappingProfile : Profile
    {
        /// <summary>
        /// order mapping constructor method
        /// </summary>
        public OrderMappingProfile()
        {
            CreateMap<dm.Orders.Order, vm.Order>();
            CreateMap<dm.Orders.Order, vm.Order>().ReverseMap();

            CreateMap<dm.Filters.OrderFilters, vm.OrderFilters>();
            CreateMap<dm.Filters.OrderFilters, vm.OrderFilters>().ReverseMap();
        }
    }
}
