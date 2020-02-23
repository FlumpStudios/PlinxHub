using AutoMapper;
using PlinxHub.Ioc.Config.Profiles;
namespace PlinxHub.Ioc.Config
{
    /// <summary>
    /// Automapper config class
    /// </summary>
    public static class MapperConfig
    {
        /// <summary>
        /// Get automapper configuration
        /// </summary>
        /// <returns></returns>
        public static MapperConfiguration Get()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<OrderMappingProfile>();
            });
        }
    }
}