
using System.Security.Claims;
namespace eStoreCA.Shared.Interfaces
{
    public interface ICurrentUserService
    {
        string UserId { get; }
        string UserName { get; }
        ClaimsPrincipal User { get; }

        #region Custom
        #endregion Custom


    }
}
