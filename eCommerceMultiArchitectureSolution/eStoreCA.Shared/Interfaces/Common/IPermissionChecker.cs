
using System.Security.Claims;
namespace eStoreCA.Shared.Interfaces
{
    public interface IPermissionChecker
    {
        bool HasClaim(string requiredClaim);




        #region Custom
        #endregion Custom


    }
}
