using AutoMapper;
using vm = PlinxHub.API.Dtos;
using dm = PlinxHub.Common.Models.Orders;

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
            CreateMap<dm.Order, vm.Order>();
            CreateMap<dm.Order, vm.Order>().ReverseMap();            
        }
    }
}
