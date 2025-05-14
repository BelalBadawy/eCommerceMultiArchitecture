
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using eStoreCA.Shared.Dtos;
using System.Reflection;

namespace eStoreCA.Application.Mapping
{
    public static class MapsterConfig
    {
        public static void RegisterMapsterConfiguration(this IServiceCollection services)
        {
            services.AddMapster();

            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());

            //var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
            //Assembly applicationAssembly = typeof(BaseDto<,>).Assembly;
            //typeAdapterConfig.Scan(applicationAssembly);
        }
    }
}
