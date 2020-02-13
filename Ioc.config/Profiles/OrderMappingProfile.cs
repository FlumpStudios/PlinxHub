using AutoMapper;
using vm = PlinxHub.API.Dtos.Response;
using dm = PlinxHub.Common.Models.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ioc.Config.Profiles
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
