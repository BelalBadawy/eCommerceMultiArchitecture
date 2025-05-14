using System.Security.Claims;
using eStoreCA.Shared.Common;
using eStoreCA.Shared.Dtos;

namespace eStoreCA.Shared.Interfaces;

public interface IAuthService
{
    Guid? UserId { get; }

    Task<MyAppResponse<AuthenticationResponse>> RefreshTokenAsync(string token);
    Task<MyAppResponse<bool>> RevokeTokenAsync(string token);
    Task<MyAppResponse<AuthenticationResponse>> RefreshTokenAsync(Guid userId);
    Task<MyAppResponse<AuthenticationResponse>> AuthenticateAsync(LoginDto loginModel);
    Task<MyAppResponse<Guid>> RegisterAsync(RegistrationDto registrationModel);
    Task<bool> IsInRoleAsync(Guid userId, string role);
    Task<bool> AuthorizeAsync(Guid userId, string policyName);

    Task<bool> AuthorizeAsync(ClaimsPrincipal user, string policyName);
    // Task<List<Claim>> GetUserClaimsAsync(ApplicationUser user);


    #region Custom

    #endregion Custom
}