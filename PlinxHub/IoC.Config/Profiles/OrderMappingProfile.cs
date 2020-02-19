using AutoMapper;
using vm = PlinxHub.API.Dtos;
using dm = PlinxHub.Common.Models.Orders;

namespace PlinxHub.Ioc.Config.Profiles
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<dm.Order, vm.Request.OrderRequest>();
            CreateMap<dm.Order, vm.Request.OrderRequest>().ReverseMap();
            CreateMap<dm.Order, vm.Response.OrderResponse>();
            CreateMap<dm.Order, vm.Response.OrderResponse>().ReverseMap();
        }
    }
}
