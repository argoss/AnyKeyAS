using AutoMapper;
using Site.Automapper;

namespace Site
{
    public class AutomapperConfig
    {
        public static void StartUp()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<ContentManagementProfile>();
            });
        }

    }
}