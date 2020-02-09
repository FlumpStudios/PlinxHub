using AutoMapper;
using PlinxHub.Ioc.Config.Profiles;
namespace PlinxHub.Ioc.Config
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