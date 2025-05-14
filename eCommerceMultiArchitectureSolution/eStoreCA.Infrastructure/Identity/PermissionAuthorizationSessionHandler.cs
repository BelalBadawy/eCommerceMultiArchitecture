
using Microsoft.AspNetCore.Authorization;
using eStoreCA.Shared.Common;
using eStoreCA.Shared.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace eStoreCA.Infrastructure.Identity
{
    public class PermissionAuthorizationSessionHandler : AuthorizationHandler<PermissionRequirement>
    {

        private readonly ISessionWrapper _sessionWrapper;

        public PermissionAuthorizationSessionHandler(ISessionWrapper sessionWrapper)
        {
            _sessionWrapper = sessionWrapper;
        }

        #region From Token





        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var stream = _sessionWrapper.GetFromSession<string>("TokenIdentityText");
            if (!string.IsNullOrEmpty(stream))
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(stream);
                var tokenS = jsonToken as JwtSecurityToken;

                var jti = tokenS.Claims.First(claim => claim.Type == "jti").Value;

                if (context.User == null)
                {
                    return Task.CompletedTask;
                }

                // If user does not have the scope claim, get out of here
                if (tokenS.Claims.Any(c => c.Type.ToUpper() == CustomClaimTypes.Permission.ToUpper() &&
                                             c.Value.ToUpper() == requirement.Permission.ToUpper()
                                               //  && c.Issuer == "http://localhost:55445"
                                               ))
                {
                    context.Succeed(requirement);
                    return Task.CompletedTask;
                }
            }
            return Task.CompletedTask;
        }
        #endregion


        #region Custom
        #endregion Custom

    }
}
