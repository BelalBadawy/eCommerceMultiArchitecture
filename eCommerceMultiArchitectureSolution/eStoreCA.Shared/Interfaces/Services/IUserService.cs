
using eStoreCA.Shared.Common;
using eStoreCA.Shared.Dtos;
namespace eStoreCA.Shared.Interfaces
{
    public interface IUserService
    {
        Task<MyAppResponse<Guid>> CreateUser(CreateUserDto request);
        Task<MyAppResponse<bool>> DeleteUser(DeleteUserDto request);
        Task<MyAppResponse<bool>> UpdateUser(UpdateUserDto request);
        Task<MyAppResponse<bool>> SetUserPassword(SetUserPasswordDto request);
        Task<MyAppResponse<List<GetAllUserDto>>> GetAllUsers(GetAllUserQueryDto request);
        Task<MyAppResponse<GetByIdUserDto>> GetUserById(GetByIdUserQueryDto request);
        Task<MyAppResponse<bool>> ForgotPassword(ForgotPasswordDto request);


        #region Custom
        #endregion Custom


    }
}
