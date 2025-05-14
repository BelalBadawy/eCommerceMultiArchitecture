
using eStoreCA.Shared.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;


namespace eStoreCA.Infrastructure.Common
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string UserName
        {
            get
            {
                return _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            }
        }
        public string UserId
        {
            get
            {
                return _httpContextAccessor.HttpContext?.User?.FindFirstValue("uid");
            }
        }

        public string TenantId
        {
            get
            {
                return _httpContextAccessor.HttpContext?.User?.FindFirstValue("tenantId");
            }
        }

        public ClaimsPrincipal User
        {
            get
            {
                return _httpContextAccessor.HttpContext?.User;
            }
        }


        public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;


        public bool IsInRole(string role)
        {
            return User?.IsInRole(role) ?? false;
        }

        public bool HasClaim(string type, string value)
        {
            return User?.HasClaim(type, value) ?? false;
        }
    }
}
