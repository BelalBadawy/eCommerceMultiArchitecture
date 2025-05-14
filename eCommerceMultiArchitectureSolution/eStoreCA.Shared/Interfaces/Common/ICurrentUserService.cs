using System.Security.Claims;

namespace eStoreCA.Shared.Interfaces;

public interface ICurrentUserService
{
    public bool IsAuthenticated { get; }
    public bool IsInRole(string role);
    public bool HasClaim(string type, string value);

    string UserId { get; }
    string UserName { get; }
    ClaimsPrincipal User { get; }
}