using eStoreCA.Application.Attributes;
using eStoreCA.Shared.Common;
using eStoreCA.Shared.Dtos;
using eStoreCA.Shared.Interfaces;
using Mediator;
namespace eStoreCA.Application.Features.Commands
{
    #region SetUserPassword Command Parameters
    [Authorize(Policy = AppPermissions.UserPermissions.Edit)]
    public class SetUserPasswordCommand : SetUserPasswordDto, IRequest<MyAppResponse<bool>>
    {

    }
    #endregion

    #region SetUserPassword Command Handler
    public class SetUserPasswordCommandHandler : IRequestHandler<SetUserPasswordCommand, MyAppResponse<bool>>
    {
        private readonly IUserService _UserService;
        public SetUserPasswordCommandHandler(IUserService UserService)
        {
            _UserService = UserService;
        }

        public async ValueTask<MyAppResponse<bool>> Handle(SetUserPasswordCommand request, CancellationToken cancellationToken)
        {
            return await _UserService.SetUserPassword(request);
        }
    }
    #endregion
}

