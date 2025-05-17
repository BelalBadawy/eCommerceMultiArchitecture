
using eStoreCA.Application.Behaviours;
using eStoreCA.Application.Mapping;
using FluentValidation;
using Mediator;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace eStoreCA.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.RegisterMapsterConfiguration();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // Fully qualify the method call to resolve ambiguity
            Microsoft.Extensions.DependencyInjection.MediatorDependencyInjectionExtensions.AddMediator(services, options =>
            {
               options.ServiceLifetime = ServiceLifetime.Scoped;
                options.Namespace = "eStoreCA.Application";
            });

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehaviour<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));

            return services;
        }
    }
}
