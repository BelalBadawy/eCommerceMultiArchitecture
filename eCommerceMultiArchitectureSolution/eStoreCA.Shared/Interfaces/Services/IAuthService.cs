
using eStoreCA.Shared.Common;
using eStoreCA.Shared.Dtos;
using System.Security.Claims;
namespace eStoreCA.Shared.Interfaces
{
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
        Task<MyAppResponse<bool>> ConfirmEmail(ConfirmEmailDto model);
        Task<MyAppResponse<bool>> ResendEMailConfirmationLink(string email);
        Task<List<Claim>> GetUserClaimsAsync(Guid userId);
        Task<MyAppResponse<bool>> ForgotUsernameOrPassword(string email);
        Task<MyAppResponse<bool>> ResetPassword(ResetPasswordDto model);



        #region Custom
        #endregion Custom


    }
}
