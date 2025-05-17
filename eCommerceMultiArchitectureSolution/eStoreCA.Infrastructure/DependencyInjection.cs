

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using eStoreCA.Domain.Entities;
using eStoreCA.Application.Interfaces;
using eStoreCA.Infrastructure.Common;
using eStoreCA.Infrastructure.Data;
using eStoreCA.Infrastructure.Data.Initializer;
using eStoreCA.Infrastructure.Identity;
using eStoreCA.Infrastructure.Identity.Services;
using eStoreCA.Shared.Common;
using eStoreCA.Shared.Interfaces;

namespace eStoreCA.Infrastructure
{
    public static class DependencyInjection
    {

        //public static readonly LoggerFactory _myLoggerFactory =
        //    new LoggerFactory(new[] {
        //        new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider()
        //    });

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<DbContext, ApplicationDbContext>();
            services.AddDbContext<ApplicationDbContext>(options =>
                 options.UseSqlServer(
                     configuration.GetConnectionString("DefaultConnection"),
                     serverOptions =>
                     {
                         serverOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                         serverOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                     }));

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();


            var emailConfig = configuration
                .GetSection("EmailConfiguration")
                .Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);


            services.AddSingleton<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IDateTimeService, DateTimeService>();

            services.AddScoped<UserManager<ApplicationUser>>();
            services.AddScoped<RoleManager<ApplicationRole>>();
            services.AddScoped<IDateTimeService, DateTimeService>();
            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddSingleton<ISessionWrapper, InMemorySessionWrapper>();
            services.AddHttpContextAccessor();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHttpClient();
            services.AddScoped<IApiRequest, ApiRequestService>();
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEmailService, MailSenderService>();
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
            services.AddScoped<IPermissionChecker, PermissionChecker>();
            // services.AddScoped<ILogUserActivityRepository, LogUserActivityRepository>();


            #region Custom
            #endregion Custom

            return services;
        }
    }
}
