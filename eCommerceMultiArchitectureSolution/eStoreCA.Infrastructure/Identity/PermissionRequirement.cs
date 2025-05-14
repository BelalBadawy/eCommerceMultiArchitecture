
using Microsoft.AspNetCore.Authorization;
namespace eStoreCA.Infrastructure.Identity
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public string Permission { get; private set; }

        public PermissionRequirement(string permission)
        {
            Permission = permission;
        }

        #region Custom
        #endregion Custom

    }
}
