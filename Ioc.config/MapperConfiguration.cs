using AutoMapper;
using Ioc.Config.Profiles;
namespace Ioc.Config
{
    public static class MapperConfig
    {
        public static MapperConfiguration Get()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<OrderMappingProfile>();    
            });
        }
    }
}