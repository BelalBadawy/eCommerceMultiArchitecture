
using eStoreCA.Shared.Common;
using eStoreCA.Shared.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace eStoreCA.Infrastructure.Identity
{
    public class PermissionSessionChecker : IPermissionChecker
    {
        private readonly ISessionWrapper _sessionWrapper;
        public PermissionSessionChecker(ISessionWrapper sessionWrapper)
        {
            _sessionWrapper = sessionWrapper;
        }

        public bool HasClaim(string requiredClaim)
        {
            var stream = _sessionWrapper.GetFromSession<string>("TokenIdentityText");
            if (!string.IsNullOrEmpty(stream))
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(stream);
                var tokenS = jsonToken as JwtSecurityToken;

                // var jti = tokenS.Claims.First(claim => claim.Type == "jti").Value;

                if (tokenS.Claims.Any(c => c.Type.ToUpper() == CustomClaimTypes.Permission.ToUpper() &&
                                           c.Value.ToUpper() == requiredClaim.ToUpper()
                    //  && c.Issuer == "http://localhost:55445"
                    ))
                {

                    return true;
                }
            }

            return false;
        }



        #region Custom
        #endregion Custom

    }
}
