
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using eStoreCA.API.Infrastructure;
using eStoreCA.Shared.Dtos;
using eStoreCA.Shared.Interfaces;




namespace eStoreCA.API.Controllers
{
     [ApiVersion("1.0")]
    public class AccountController : BaseApiController
    {
        private readonly IAuthService _authService;



     
 public AccountController(IAuthService authService)
        {
            _authService = authService;
        }




       
       
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> RegisterAccount(RegistrationDto registrationModel)
        {

            if (registrationModel == null)
            {
                return BadRequest(ModelState);
            }

            var response = await _authService.RegisterAsync(registrationModel);

             return ActionResult(response);
          
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginModel)
        {

           if (loginModel == null)
            {
                return BadRequest(ModelState);
            }

            var response = await _authService.AuthenticateAsync(loginModel);

            if (response.Succeeded)
            {
                if (response.Data != null && !string.IsNullOrEmpty(response.Data.RefreshToken))
                {
                    SetRefreshTokenInCookie(response.Data.RefreshToken, response.Data.RefreshTokenExpiration);
                }
            }

             return ActionResult(response);
          
        }

[AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailDto model)
        {
            var response = await _authService.ConfirmEmail(model);

            return ActionResult(response);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ResendEMailConfirmationLink(string email)
        {
            var response = await _authService.ResendEMailConfirmationLink(email);

            return ActionResult(response);
        }



        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var response = await _authService.ForgotUsernameOrPassword(email);

            return ActionResult(response);
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto model)
        {
            var response = await _authService.ResetPassword(model);

            return ActionResult(response);
        }

  //      [AllowAnonymous]
  //       [HttpGet()]
  //public async Task<IActionResult> RefreshToken()
  //{
  //    var refreshToken = Request.Cookies["refreshToken"];

  //    var response = await _authService.RefreshTokenAsync(refreshToken);

  //    if (response.Succeeded)
  //    {
  //        if (response.Data != null && !string.IsNullOrEmpty(response.Data.RefreshToken))
  //        {
  //            SetRefreshTokenInCookie(response.Data.RefreshToken, response.Data.RefreshTokenExpiration);
  //        }
  //    }

  //     return ActionResult(response);
  //}

      [AllowAnonymous]
       [HttpGet()]
public async Task<IActionResult> RefreshToken(string refreshToken)
{
   if (string.IsNullOrEmpty(refreshToken))
{
    refreshToken = Request.Cookies["refreshToken"];
}

if (string.IsNullOrEmpty(refreshToken))
{
    return ReturnActionResult(null, false, null, "Token is required!", null);
}
    var response = await _authService.RefreshTokenAsync(refreshToken);

    if (response.Succeeded)
    {
        if (response.Data != null && !string.IsNullOrEmpty(response.Data.RefreshToken))
        {
            SetRefreshTokenInCookie(response.Data.RefreshToken, response.Data.RefreshTokenExpiration);
        }
    }

     return ActionResult(response);
}



[HttpPost()]
public async Task<IActionResult> RevokeToken([FromBody] RevokeToken model)
{
    var token = model.Token ?? Request.Cookies["refreshToken"];

    if (string.IsNullOrEmpty(token))
    {
        return ReturnActionResult(null, false,null, "Token is required!", null);
    }

    var response = await _authService.RevokeTokenAsync(token);

     return ActionResult(response);
}



         private void SetRefreshTokenInCookie(string refreshToken, DateTime expires)
  {
      if (!string.IsNullOrEmpty(refreshToken))
      {
          var cookieOptions = new CookieOptions
          {
              HttpOnly = true,
              Expires = expires.ToLocalTime(),
              Secure = true,
              IsEssential = true,
              SameSite = SameSiteMode.None
          };
          Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
      }
  }

            
#region Custom
#endregion Custom

}
}
