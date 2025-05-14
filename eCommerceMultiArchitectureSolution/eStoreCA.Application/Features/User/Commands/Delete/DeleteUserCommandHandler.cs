using eStoreCA.Application.Attributes;
using eStoreCA.Shared.Common;
using eStoreCA.Shared.Dtos;
using eStoreCA.Shared.Interfaces;
using Mediator;
namespace eStoreCA.Application.Features.Commands

{
    #region Create Command Parameters
    [Authorize(Policy = AppPermissions.UserPermissions.Delete)]
    public class DeleteUserCommand : DeleteUserDto, IRequest<MyAppResponse<bool>>
    {
       
    }
    #endregion

    #region Delete Command Handler
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, MyAppResponse<bool>>
    {
        private readonly IUserService _UserService;

        public DeleteUserCommandHandler(IUserService UserService)
        {
            _UserService = UserService;
        }

        public async ValueTask<MyAppResponse<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            return await _UserService.DeleteUser(request);
        }
    }
    #endregion
}





