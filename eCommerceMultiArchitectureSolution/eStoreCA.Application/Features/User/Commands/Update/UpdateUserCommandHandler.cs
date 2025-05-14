using eStoreCA.Application.Attributes;
using eStoreCA.Shared.Common;
using eStoreCA.Shared.Dtos;
using eStoreCA.Shared.Interfaces;
using Mediator;
namespace eStoreCA.Application.Features.Commands
{
    #region Update Command Parameters
    [Authorize(Policy = AppPermissions.UserPermissions.Edit)]
    public class UpdateUserCommand : UpdateUserDto, IRequest<MyAppResponse<bool>>
    {

    }
    #endregion

    #region Update Command Handler
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, MyAppResponse<bool>>
    {
        private readonly IUserService _UserService;

        public UpdateUserCommandHandler(IUserService UserService)
        {
            _UserService = UserService;
        }

        public async ValueTask<MyAppResponse<bool>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            return await _UserService.UpdateUser(request);
        }
    }
    #endregion
}



