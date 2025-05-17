

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using eStoreCA.Domain.Entities;
using eStoreCA.Application.Interfaces;
using eStoreCA.Shared.Common;
using eStoreCA.Shared.Dtos;
using eStoreCA.Shared.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

	




namespace eStoreCA.Infrastructure.Identity.Services
{
   public class AuthService : IAuthService
    {
        public Guid? UserId => null;


        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtConfig _jwtConfig;
        private readonly IAuthorizationService _authorizationService;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;


	


 public AuthService(
                   UserManager<ApplicationUser> userManager,
                   IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
                   IOptions<JwtConfig> JwtConfig,
                   SignInManager<ApplicationUser> signInManager,
                   IAuthorizationService authorizationService,
                   RoleManager<ApplicationRole> roleManager,
                   IApplicationDbContext dbContext,
                   IHttpContextAccessor httpContextAccessor,
                   IConfiguration configuration,
                   IEmailService emailService)
        {
            _userManager = userManager;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            _jwtConfig = JwtConfig.Value;
            _signInManager = signInManager;
            _authorizationService = authorizationService;
            _roleManager = roleManager;
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _emailService = emailService;
        }





       




        public async Task<MyAppResponse<AuthenticationResponse>> AuthenticateAsync(LoginDto loginModel)
        {

var dtoValidator = new LoginDtoValidator();

var validationResult = dtoValidator.Validate(loginModel);

if (validationResult != null && validationResult.IsValid == false)
{
    return new MyAppResponse<AuthenticationResponse>(errors: validationResult.Errors.Select(o => o.ErrorMessage).ToList());
}




            var user = await _userManager.FindByEmailAsync(loginModel.Email);

            if (user == null)
            {
               

  

                return new MyAppResponse<AuthenticationResponse>($"User with {loginModel.Email} not found in.", statusCode: HttpStatusCode.NotFound);

            }



         if (user.EmailConfirmed == false)
             return new MyAppResponse<AuthenticationResponse>("Please confirm your email.", statusCode: HttpStatusCode.Unauthorized);


           var result = await _signInManager.PasswordSignInAsync(user.UserName, loginModel.Password, false, lockoutOnFailure: false);


        if (result.IsLockedOut)
        {
            return new MyAppResponse<AuthenticationResponse>(string.Format("Your account has been locked. You should wait until {0} (UTC time) to be able to login", user.LockoutEnd), statusCode: HttpStatusCode.Unauthorized);
        }

          if (!result.Succeeded)
{
  // Increamenting AccessFailedCount of the AspNetUser by 1
  await _userManager.AccessFailedAsync(user);

  if (user.AccessFailedCount >= SD.MaximumLoginAttempts)
  {
      // Lock the user for one day
      await _userManager.SetLockoutEndDateAsync(user, DateTime.UtcNow.AddDays(1));

      return new MyAppResponse<AuthenticationResponse>($"Your account has been locked. You should wait until {user.LockoutEnd} (UTC time) to be able to login", statusCode: HttpStatusCode.Unauthorized);
  }

  return new MyAppResponse<AuthenticationResponse>("Invalid username or password", statusCode: HttpStatusCode.Unauthorized);

}


             
            var response = await GetUserTokenWithAsync(user, null);
          

            return new MyAppResponse<AuthenticationResponse>(response);
        }

  public async Task<MyAppResponse<bool>> RevokeTokenAsync(string token)
 {

     var refreshToken = await _dbContext.RefreshTokens.FirstOrDefaultAsync(t => t.Token == token);

     if (!refreshToken.IsActive)
     {
         return new MyAppResponse<bool>($"Invalid token.");
     }


     refreshToken.RevokedOn = DateTime.UtcNow;

     _dbContext.RefreshTokens.Update(refreshToken);
     await _dbContext.SaveChangesAsync(default);

     return new MyAppResponse<bool>(true);
 }


      public async Task<MyAppResponse<AuthenticationResponse>> RefreshTokenAsync(string token)
 {
     AuthenticationResponse authModel = new AuthenticationResponse();

     var refreshToken = await _dbContext.RefreshTokens.AsNoTracking().FirstOrDefaultAsync(u => u.Token == token);
     if (refreshToken == null || !refreshToken.IsActive)
     {
         return new MyAppResponse<AuthenticationResponse>($"Invalid token.", statusCode: HttpStatusCode.Unauthorized);
     }
     
     refreshToken.RevokedOn = DateTime.UtcNow;

     var newRefreshToken = GenerateRefreshToken();
     newRefreshToken.ApplicationUserId = refreshToken.ApplicationUserId;
     await _dbContext.RefreshTokens.AddAsync(newRefreshToken);
     await _dbContext.SaveChangesAsync(default);

     var user = await _userManager.FindByIdAsync(refreshToken.ApplicationUserId.ToString());
     if (user == null)
     {
         return new MyAppResponse<AuthenticationResponse>($"Invalid token.", statusCode: HttpStatusCode.Unauthorized);
     }

     if (user.LockoutEnd != null && user.LockoutEnd > DateTime.UtcNow)
     {
         return new MyAppResponse<AuthenticationResponse>($"You have been locked out.", statusCode: HttpStatusCode.Unauthorized);
     }



     var jwtSecurityToken = await GenerateToken(user);

     authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
     authModel.Email = user.Email;
     authModel.UserName = user.UserName;
     authModel.Id = user.Id.ToString();
     authModel.RefreshToken = newRefreshToken.Token;
     authModel.RefreshTokenExpiration = newRefreshToken.ExpiresOn;

     return new MyAppResponse<AuthenticationResponse>(authModel);
 }

 public async Task<MyAppResponse<AuthenticationResponse>> RefreshTokenAsync(Guid userid)
 {
     AuthenticationResponse authModel = new AuthenticationResponse();

 var refreshToken = await _dbContext.RefreshTokens.AsNoTracking()
     .FirstOrDefaultAsync(u => u.ApplicationUserId == userid && u.RevokedOn == null && u.ExpiresOn.Date > DateTime.Now.Date);
 if (refreshToken == null || !refreshToken.IsActive)
 {
     return new MyAppResponse<AuthenticationResponse>($"Invalid token.", statusCode: HttpStatusCode.Unauthorized);
 }

 refreshToken.RevokedOn = DateTime.UtcNow;

 var newRefreshToken = GenerateRefreshToken();
 newRefreshToken.ApplicationUserId = refreshToken.ApplicationUserId;
 await _dbContext.RefreshTokens.AddAsync(newRefreshToken);
 await _dbContext.SaveChangesAsync(default);

 var user = await _userManager.FindByIdAsync(refreshToken.ApplicationUserId.ToString());
 if (user == null)
 {
     return new MyAppResponse<AuthenticationResponse>($"Invalid token.", statusCode: HttpStatusCode.Unauthorized);
 }

 if (user.LockoutEnd != null && user.LockoutEnd > DateTime.UtcNow)
 {
     return new MyAppResponse<AuthenticationResponse>($"You have been locked out.", statusCode: HttpStatusCode.Unauthorized);
 }



 var jwtSecurityToken = await GenerateToken(user);

 authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
 authModel.Email = user.Email;
 authModel.UserName = user.UserName;
 authModel.Id = user.Id.ToString();
 authModel.RefreshToken = newRefreshToken.Token;
 authModel.RefreshTokenExpiration = newRefreshToken.ExpiresOn;

 return new MyAppResponse<AuthenticationResponse>(authModel);
 }

        public async Task<MyAppResponse<Guid>> RegisterAsync(RegistrationDto request)
        {
            
            var dtoValidator = new RegistrationDtoValidator();

            var validationResult = dtoValidator.Validate(request);

            if (validationResult != null && validationResult.IsValid == false)
            {
                return new MyAppResponse<Guid>(errors: validationResult.Errors.Select(o => o.ErrorMessage).ToList());
            }



           var user = new ApplicationUser
            {
                Email = request.Email,
                FullName = request.FullName,
                EmailConfirmed = false,
                UserName = request.Email,
                CreatedDate = DateTime.Now
            


};
           var existingEmail = await _userManager.FindByEmailAsync(request.Email);
 

  if (existingEmail == null)
  {
      var result = await _userManager.CreateAsync(user, request.Password);

      if (result.Succeeded)
      {
          try
          {
              if (await SendConfirmEMailAsync(user))
              {
                  return new MyAppResponse<Guid>(user.Id, message: "Email sent successfully please check your email");
              }
              else
              {
                  return new MyAppResponse<Guid>(message: "Failed to send email. Please contact admin", statusCode: HttpStatusCode.BadRequest);
              }
          }
          catch (Exception)
          {
              return new MyAppResponse<Guid>(message: "Failed to send email. Please contact admin", statusCode: HttpStatusCode.BadRequest);
          }
      }
      else
      {
          return new MyAppResponse<Guid>($"{string.Join("; ", result.Errors.Select(o => o.Description).ToList())}");
      }
  }
  else
  {
      return new MyAppResponse<Guid>($"Email {request.Email} already exists.");
  }
        
        }

        private async Task<AuthenticationResponse> GetUserTokenWithAsync(ApplicationUser user, RefreshToken userRefreshToken)
        {
            JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

            AuthenticationResponse response = new AuthenticationResponse
            {
                Id = user.Id.ToString(),
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Email = user.Email,
                UserName = user.UserName
            };

           
            var activeRefreshToken = await _dbContext.RefreshTokens.AsNoTracking()
                .FirstOrDefaultAsync(t =>
                    t.ExpiresOn < DateTime.UtcNow &&
                    t.RevokedOn == null  &&
                    t.ApplicationUserId == user.Id);
            if (activeRefreshToken != null)
            {
                response.RefreshToken = activeRefreshToken.Token;
                response.RefreshTokenExpiration = activeRefreshToken.ExpiresOn;
            }
            else
            {
                var refreshToken = GenerateRefreshToken();

                response.RefreshToken = refreshToken.Token;
                response.RefreshTokenExpiration = refreshToken.ExpiresOn;
                refreshToken.ApplicationUserId = user.Id;
                await _dbContext.RefreshTokens.AddAsync(refreshToken);
                await _dbContext.SaveChangesAsync(default);
            }

            return response;
        }


        public async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {
            if (_jwtConfig != null)
            {
                var userClaims = await _userManager.GetClaimsAsync(user);
                var roles = await _userManager.GetRolesAsync(user);

                var roleClaims = new List<Claim>();
                var claimsOfRoles = new List<Claim>();

                for (int i = 0; i < roles.Count; i++)
                {
                    roleClaims.Add(new Claim("roles", roles[i]));

                    var role = await _roleManager.FindByNameAsync(roles[i]);
                    if (role != null)
                    {
                        var result = await _roleManager.GetClaimsAsync(role);
                        if (result != null)
                        {
                            foreach (var cr in result)
                            {
                                claimsOfRoles.Add(new Claim(CustomClaimTypes.Permission.ToUpper(), cr.Value.ToUpper()));
                            }
                        }
                    }
                }


                var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Email, user.Email),
                        new Claim("uid", user.Id.ToString()),

                       new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.Email)
                    }
                    .Union(userClaims)
                    .Union(roleClaims)
                    .Union(claimsOfRoles);

                var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Key));
                var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

                var jwtSecurityToken = new JwtSecurityToken(
                    issuer: _jwtConfig.Issuer,
                    audience: _jwtConfig.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(_jwtConfig.DurationInMinutes),
                    signingCredentials: signingCredentials);

                return jwtSecurityToken;
            }

            return new JwtSecurityToken();



        }


       private RefreshToken GenerateRefreshToken()
        {
            var randomNumber = new byte[32];

            using var generator = new RNGCryptoServiceProvider();

            generator.GetBytes(randomNumber);

            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                ExpiresOn = DateTime.UtcNow.AddDays(1),
                CreatedOn = DateTime.UtcNow
            };
        }

        public async Task<bool> IsInRoleAsync(Guid userId, string role)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == userId);

            if (user != null)
            {
                return await _userManager.IsInRoleAsync(user, role);
            }

            return false;
        }

        public async Task<bool> AuthorizeAsync(Guid userId, string policyName)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == userId);
            if (user != null)
            {
                var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

                var result = await _authorizationService.AuthorizeAsync(principal, policyName);

                return result.Succeeded;
            }

            return false;
        }

        public async Task<bool> AuthorizeAsync(ClaimsPrincipal user, string policyName)
        {

            var result = await _authorizationService.AuthorizeAsync(user, policyName);

            return result.Succeeded;

        }

public async Task<List<Claim>> GetUserClaimsAsync(ApplicationUser user)
        {
            if (user != null)
            {
                var userClaims = await _userManager.GetClaimsAsync(user);
                var roles = await _userManager.GetRolesAsync(user);

                var roleClaims = new List<Claim>();
                var claimsOfRoles = new List<Claim>();

                for (int i = 0; i < roles.Count; i++)
                {
                    roleClaims.Add(new Claim("roles", roles[i]));

                    var role = await _roleManager.FindByNameAsync(roles[i]);
                    if (role != null)
                    {
                        var result = await _roleManager.GetClaimsAsync(role);
                        if (result != null)
                        {
                            foreach (var cr in result)
                            {
                                claimsOfRoles.Add(new Claim(CustomClaimTypes.Permission.ToUpper(), cr.Value.ToUpper()));
                            }
                        }
                    }
                }


                var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Email, user.Email),
                        new Claim("uid", user.Id.ToString()),

                       new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                    }
                    .Union(userClaims)
                    .Union(roleClaims)
                    .Union(claimsOfRoles);


                return claims.ToList();
            }

            return null;
        }


         public async Task<MyAppResponse<bool>> ConfirmEmail(ConfirmEmailDto model)
 {
     var dtoValidator = new ConfirmEmailDtoValidator();

     var validationResult = dtoValidator.Validate(model);

     if (validationResult != null && validationResult.IsValid == false)
     {
         return new MyAppResponse<bool>(errors: validationResult.Errors.Select(o => o.ErrorMessage).ToList());
     }



     var user = await _userManager.FindByEmailAsync(model.Email);

     if (user == null)
         return new MyAppResponse<bool>($"This email address has not been registered yet", statusCode: HttpStatusCode.Unauthorized);


     if (user.EmailConfirmed == true)
         return new MyAppResponse<bool>($"Your email was confirmed before. Please login to your account", statusCode: HttpStatusCode.BadRequest);

     try
     {
         var decodedTokenBytes = WebEncoders.Base64UrlDecode(model.Token);

         var decodedToken = Encoding.UTF8.GetString(decodedTokenBytes);

         var result = await _userManager.ConfirmEmailAsync(user, decodedToken);

         if (result.Succeeded)
         {
             return new MyAppResponse<bool>(true, $"Your email address is confirmed. You can login now");
         }

         return new MyAppResponse<bool>($"Invalid token. Please try again", statusCode: HttpStatusCode.BadRequest);

     }
     catch (Exception)
     {
         return new MyAppResponse<bool>($"Invalid token. Please try again", statusCode: HttpStatusCode.BadRequest);
     }
 }

        public async Task<List<Claim>> GetUserClaimsAsync(Guid userId)
        {
            var user = await _userManager.FindByEmailAsync(userId.ToString());

            if (user != null)
            {
                var userClaims = await _userManager.GetClaimsAsync(user);
                var roles = await _userManager.GetRolesAsync(user);

                var roleClaims = new List<Claim>();
                var claimsOfRoles = new List<Claim>();

                for (int i = 0; i < roles.Count; i++)
                {
                    roleClaims.Add(new Claim("roles", roles[i]));

                    var role = await _roleManager.FindByNameAsync(roles[i]);
                    if (role != null)
                    {
                        var result = await _roleManager.GetClaimsAsync(role);
                        if (result != null)
                        {
                            foreach (var cr in result)
                            {
                                claimsOfRoles.Add(new Claim(CustomClaimTypes.Permission.ToUpper(), cr.Value.ToUpper()));
                            }
                        }
                    }
                }


                var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Email, user.Email),
                        new Claim("uid", user.Id.ToString()),
                       new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                    }
                    .Union(userClaims)
                    .Union(roleClaims)
                    .Union(claimsOfRoles);


                return claims.ToList();
            }

            return null;
        }

        public async Task<MyAppResponse<bool>> ForgotUsernameOrPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
                return new MyAppResponse<bool>($"Invalid email");

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return new MyAppResponse<bool>($"This email address has not been registered yet");

            if (user.EmailConfirmed == false)
                return new MyAppResponse<bool>("Please confirm your email address first.");

            try
            {
                if (await SendForgotUsernameOrPasswordEmail(user))
                {
                    return new MyAppResponse<bool>(true, "Forgot username or password email sent. Please check your email");
                }

                return new MyAppResponse<bool>("Failed to send email. Please contact admin");

            }
            catch (Exception)
            {
                return new MyAppResponse<bool>("Failed to send email. Please contact admin");
            }
        }

        public async Task<MyAppResponse<bool>> ResendEMailConfirmationLink(string email)
        {
            if (string.IsNullOrEmpty(email))
                return new MyAppResponse<bool>(message: "Invalid email", statusCode: HttpStatusCode.BadRequest);


            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return new MyAppResponse<bool>($"This email address has not been registered yet", statusCode: HttpStatusCode.Unauthorized);

            if (user.EmailConfirmed == true)
                return new MyAppResponse<bool>($"Your email was confirmed before. Please login to your account", statusCode: HttpStatusCode.BadRequest);

            try
            {
                if (await SendConfirmEMailAsync(user))
                {
                    return new MyAppResponse<bool>(true, $"Email sent successfully please check your email");
                }
                else
                {
                    return new MyAppResponse<bool>(message: "Failed to send email. Please contact admin", statusCode: HttpStatusCode.BadRequest);
                }
            }
            catch (Exception)
            {
                return new MyAppResponse<bool>(message: "Failed to send email. Please contact admin", statusCode: HttpStatusCode.BadRequest);
            }
        }

        public async Task<MyAppResponse<bool>> ResetPassword(ResetPasswordDto model)
        {
            var dtoValidator = new ResetPasswordDtoValidator();

            var validationResult = dtoValidator.Validate(model);

            if (validationResult != null && validationResult.IsValid == false)
            {
                return new MyAppResponse<bool>(errors: validationResult.Errors.Select(o => o.ErrorMessage).ToList());
            }

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
                return new MyAppResponse<bool>($"This email address has not been registered yet");


            if (user.EmailConfirmed == false)
                return new MyAppResponse<bool>("PLease confirm your email address first");

            try
            {
                var decodedTokenBytes = WebEncoders.Base64UrlDecode(model.Token);
                var decodedToken = Encoding.UTF8.GetString(decodedTokenBytes);

                var result = await _userManager.ResetPasswordAsync(user, decodedToken, model.Password);

                if (result.Succeeded)
                {
                    return new MyAppResponse<bool>(true, $"Your password has been reset");
                }

                return new MyAppResponse<bool>($"Invalid token. Please try again");

            }
            catch (Exception)
            {
                return new MyAppResponse<bool>($"Invalid token. Please try again");

            }
        }

        private async Task<bool> SendConfirmEMailAsync(ApplicationUser user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            var url = $"{_configuration["AppConfig:AppUrl"]}/{_configuration["AppConfig:ConfirmEmailPath"]}?token={token}&email={user.Email}";

            var body = $"<p>Hello: {user.FullName}</p>" +
                       "<p>Please confirm your email address by clicking on the following link.</p>" +
                       $"<p><a href=\"{url}\">Click here</a></p>" +
                       "<p>Thank you,</p>" +
                       $"<br>{_configuration["AppConfig:ApplicationName"]}";

            var emailSend = new SendEmailDto() { MailTo = user.Email, Subject = "Please confirm your email address", MessageBody = body };

            var result = await _emailService.SendAsync(emailSend);

            if (string.IsNullOrEmpty(result))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private async Task<bool> SendForgotUsernameOrPasswordEmail(ApplicationUser user)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            var url = $"{_configuration["AppConfig:AppUrl"]}/{_configuration["AppConfig:ResetPasswordPath"]}?token={token}&email={user.Email}";

            var body = $"<p>Hello: {user.FullName}</p>" +
                       $"<p>Username: {user.UserName}.</p>" +
                       "<p>In order to reset your password, please click on the following link.</p>" +
                       $"<p><a href=\"{url}\">Click here</a></p>" +
                       "<p>Thank you,</p>" +
                       $"<br>{_configuration["AppConfig:ApplicationName"]}";

            var emailSend = new SendEmailDto() { MailTo = user.Email, Subject = "Forgot username or password", MessageBody = body };


            var result = await _emailService.SendAsync(emailSend);

            if (string.IsNullOrEmpty(result))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

 
#region Custom
#endregion Custom

}
}
