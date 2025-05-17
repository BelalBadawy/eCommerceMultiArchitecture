

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using eStoreCA.Domain.Entities;
using eStoreCA.Shared.Common;
using eStoreCA.Shared.Dtos;
using eStoreCA.Shared.Interfaces;


namespace eStoreCA.Infrastructure.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(UserManager<ApplicationUser> userManager, IEmailService emailService, IHttpContextAccessor contextAccessor)
        {
            _userManager = userManager;
            _emailService = emailService;
            _httpContextAccessor = contextAccessor;

            #region Custom Constructor
            #endregion Custom Constructor

        }

        public async Task<MyAppResponse<Guid>> CreateUser(CreateUserDto request)
        {
           // var newUserName = request.UserName.Trim();

 
var existUser = await _userManager.FindByNameAsync(request.UserName.Trim());

            

            if (existUser != null)
            {
                return new MyAppResponse<Guid>(string.Format(SD.ExistData, request.UserName));
            }

 
existUser = await _userManager.FindByEmailAsync(request.Email.Trim());

            

            if (existUser != null)
            {
                return new MyAppResponse<Guid>(string.Format(SD.ExistData, request.Email));
            }

            ApplicationUser applicationUser = new ApplicationUser();

            applicationUser.FullName = request.FullName.Trim();
            applicationUser.Email = request.Email.Trim(); 
            applicationUser.PhoneNumber = request.PhoneNumber.Trim();
            applicationUser.UserName = request.UserName.Trim();
            applicationUser.CreatedDate = DateTime.Now;

 

            

            var result = await _userManager.CreateAsync(applicationUser);

            if (result.Succeeded)
            {

var newUser = await _userManager.FindByEmailAsync(request.Email);

                

                if (request.Roles != null && request.Roles.Any())
                {
                    if (newUser != null)
                    {
                        if (request.Roles != null && request.Roles.Any())
                        {
                            await _userManager.AddToRolesAsync(newUser, request.Roles);
                        }
                    }
                }

                return new MyAppResponse<Guid>(data: newUser.Id);
            }
            else
            {
                MyAppResponse<Guid> myAppResponse = new MyAppResponse<Guid>();
                myAppResponse.Succeeded = false;
                myAppResponse.Errors = new List<string>();
                foreach (var error in result.Errors)
                {
                    myAppResponse.Errors.Add(error.Description);
                }

                return myAppResponse;
            }
        }

        public async Task<MyAppResponse<bool>> DeleteUser(DeleteUserDto request)
        {

            
var result = await _userManager.FindByIdAsync(request.Id.ToString());

            

            if (result != null)
            {
                await _userManager.DeleteAsync(result);

                return new MyAppResponse<bool>(true);
            }

            return new MyAppResponse<bool>(false);
        }

        public async Task<MyAppResponse<List<GetAllUserDto>>> GetAllUsers(GetAllUserQueryDto request)
        {
            List<GetAllUserDto> dtos = new List<GetAllUserDto>();

            List<ApplicationUser> result = null;


  
 result = await _userManager.Users.ToListAsync();

           


            if (result != null && result.Any())
            {
                if (!string.IsNullOrEmpty(request.Search))
                {
                    result = result.Where(o =>
                        o.UserName.Contains(request.Search) ||
                        o.Email.Contains(request.Search)).ToList();
                }

                if (!string.IsNullOrEmpty(request.Sort))
                {
                    switch (request.Sort.ToLower())
                    {

                        case "id":
                            if (request.AscendingOrder)
                            {
                                result = result.OrderBy(o => o.Id).ToList();
                            }
                            else
                            {
                                result = result.OrderByDescending(o => o.Id).ToList();
                            }
                            break;

                        case "username":
                            if (request.AscendingOrder)
                            {
                                result = result.OrderBy(o => o.UserName).ToList();
                            }
                            else
                            {
                                result = result.OrderByDescending(o => o.UserName).ToList();
                            }

                            break;
                        case "email":
                            if (request.AscendingOrder)
                            {
                                result = result.OrderBy(o => o.Email).ToList();
                            }
                            else
                            {
                                result = result.OrderByDescending(o => o.Email).ToList();
                            }
                            break;
                        case "fullname":
                            if (request.AscendingOrder)
                            {
                                result = result.OrderBy(o => o.FullName).ToList();
                            }
                            else
                            {
                                result = result.OrderByDescending(o => o.FullName).ToList();
                            }
                            break;

                            #region Custom Switch

                            #endregion Custom Switch


                    }
                }

                foreach (var r in result)
                {
                    GetAllUserDto dto = new GetAllUserDto();
                    dto.Id = r.Id;
                    dto.Email = r.Email;
                    dto.FullName = r.FullName;
                    dto.EmailConfirmed = r.EmailConfirmed;
                    dto.PhoneNumber = r.PhoneNumber;
                    dto.PhoneNumberConfirmed = r.PhoneNumberConfirmed;
                    dto.UserName = r.UserName;

                    var roles = await _userManager.GetRolesAsync(r);
                    dto.UserRoles = roles.ToList();

                    dtos.Add(dto);
                }
            }

            return new MyAppResponse<List<GetAllUserDto>>(dtos);
        }



        public async Task<MyAppResponse<GetByIdUserDto>> GetUserById(GetByIdUserQueryDto request)
        {
            GetByIdUserDto dto = null;

            ApplicationUser result = null;


result = await _userManager.FindByIdAsync(request.Id.ToString());

            

            if (result != null)
            {
                dto = new GetByIdUserDto();
                dto.Id = result.Id;
                dto.Email = result.Email;
                dto.FullName = result.FullName;
                dto.EmailConfirmed = result.EmailConfirmed;
                dto.PhoneNumber = result.PhoneNumber;
                dto.PhoneNumberConfirmed = result.PhoneNumberConfirmed;
                dto.UserName = result.UserName;

                var roles = await _userManager.GetRolesAsync(result);
                dto.UserRoles = roles.ToList();
            }

            return new MyAppResponse<GetByIdUserDto>(dto);
        }

        public async Task<MyAppResponse<bool>> UpdateUser(UpdateUserDto request)
        {


var originUser = await _userManager.FindByIdAsync(request.Id.ToString());


            

            if (originUser != null)
            {
                var existUser = await _userManager.Users.FirstOrDefaultAsync(o =>
                   (o.NormalizedUserName == request.UserName.ToUpper() ||
                     o.NormalizedEmail == request.Email.ToUpper()) &&
                    o.Id != request.Id

);

                if (existUser != null)
                {
                    if (existUser.NormalizedUserName == request.UserName.ToUpper())
                    {
                        return new MyAppResponse<bool>(string.Format(SD.ExistData, request.UserName));
                    }

                    if (existUser.NormalizedEmail == request.Email.ToUpper())
                    {
                        return new MyAppResponse<bool>(string.Format(SD.ExistData, request.Email));
                    }

                }


                originUser.FullName = request.FullName;
                originUser.Email = request.Email;
                originUser.PhoneNumber = request.PhoneNumber;
                originUser.UserName = request.UserName;

                var result = await _userManager.UpdateAsync(originUser);

                if (result.Succeeded)
                {
                    var userRoles = await _userManager.GetRolesAsync(originUser);

                    await _userManager.RemoveFromRolesAsync(originUser, userRoles);

                    if (request.Roles != null && request.Roles.Any())
                    {
                        if (originUser != null)
                        {
                            if (request.Roles != null && request.Roles.Any())
                            {
                                await _userManager.AddToRolesAsync(originUser, request.Roles);
                            }
                        }
                    }

                    return new MyAppResponse<bool>(data: true);
                }
                else
                {
                    MyAppResponse<bool> myAppResponse = new MyAppResponse<bool>();
                    myAppResponse.Succeeded = false;
                    myAppResponse.Errors = new List<string>();
                    foreach (var error in result.Errors)
                    {
                        myAppResponse.Errors.Add(error.Description);
                    }

                    return myAppResponse;
                }
            }
            else
            {
                MyAppResponse<bool> myAppResponse = new MyAppResponse<bool>();
                myAppResponse.Succeeded = false;
                myAppResponse.Message = "This user doesn't exist.";

                return myAppResponse;
            }
        }

        public async Task<MyAppResponse<bool>> SetUserPassword(SetUserPasswordDto request)
        {


var originUser = await _userManager.FindByIdAsync(request.Id.ToString());

            

            if (originUser != null)
            {
                string resetToken = await _userManager.GeneratePasswordResetTokenAsync(originUser);

                var result = await _userManager.ResetPasswordAsync(originUser, resetToken, request.Password);

                if (result.Succeeded)
                {
                    return new MyAppResponse<bool>(true);
                }
                else
                {
                    return new MyAppResponse<bool>(message: "Error setting password");
                }
            }
            return new MyAppResponse<bool>(message: "Error finding user");
        }

        public async Task<MyAppResponse<bool>> ForgotPassword(ForgotPasswordDto request)
        {

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                // Don't reveal that the user does not exist or is not confirmed
                return new MyAppResponse<bool>(message: "This email doesn't exist.");
            }


            if (user.EmailConfirmed == false)
            {
                return new MyAppResponse<bool>(message: "This email is not confirmed.");

            }

            var urlPath = _httpContextAccessor.HttpContext.Request.PathBase;

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = urlPath + $"/Account/ResetPassword?userId={user.Id}&code={code}";

            SendEmailDto emailModel = new SendEmailDto();

            emailModel.Subject = "Reset Password";
            emailModel.MessageBody = "Please reset your password by clicking here: <a href=\"" + callbackUrl + "\">link</a>";
            emailModel.MailTo = user.Email;

            try
            {
                var emailResult = await _emailService.SendAsync(emailModel);

                if (string.IsNullOrEmpty(emailResult))
                {
                    return new MyAppResponse<bool>(data: true);
                }
                else
                {
                    return new MyAppResponse<bool>(message: emailResult);
                }
            }
            catch (Exception e)
            {
                return new MyAppResponse<bool>(message: e.Message);

            }
        }
    }
}

