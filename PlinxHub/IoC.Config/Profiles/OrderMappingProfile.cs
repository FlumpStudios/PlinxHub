using AutoMapper;
using vm = PlinxHub.API.Dtos;
using dm = PlinxHub.Common.Models.Orders;

namespace PlinxHub.Ioc.Config.Profiles
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<dm.Order, vm.Order>();
            CreateMap<dm.Order, vm.Order>().ReverseMap();            
        }
    }
}
