
using Microsoft.AspNetCore.Http;
using eStoreCA.Shared.Common;
using eStoreCA.Shared.Interfaces;

namespace eStoreCA.Infrastructure.Identity
{
    public class PermissionChecker : IPermissionChecker
    {
        private readonly IHttpContextAccessor _context;
        public PermissionChecker(IHttpContextAccessor context)
        {
            _context = context;
        }
        #region From Token

        public bool HasClaim(string requiredClaim)
        {
            if (_context.HttpContext.User == null)
            {
                return false;
            }

            // If user does not have the scope claim, get out of here
            if (_context.HttpContext.User.HasClaim(c => c.Type == CustomClaimTypes.Permission &&
                                                        c.Value.ToUpper() == requiredClaim.ToUpper()))
            {
                return true;
            }

            return false;
        }
        #endregion


        #region Custom
        #endregion Custom

    }
}
