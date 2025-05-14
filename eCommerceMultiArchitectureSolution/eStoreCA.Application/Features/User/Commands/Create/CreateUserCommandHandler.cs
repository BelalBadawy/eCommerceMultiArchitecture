using eStoreCA.Application.Attributes;
using eStoreCA.Shared.Common;
using eStoreCA.Shared.Dtos;
using eStoreCA.Shared.Interfaces;
using Mediator;
namespace eStoreCA.Application.Features.Commands
{

    #region Create Command Parameters
    [Authorize(Policy = AppPermissions.UserPermissions.Create)]
    public class CreateUserCommand : CreateUserDto, IRequest<MyAppResponse<Guid>>
    {
    }
    #endregion

    #region Create Command Handler
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, MyAppResponse<Guid>>
    {
        private readonly IUserService _UserService;
        public CreateUserCommandHandler(IUserService UserService)
        {
            _UserService = UserService;
        }
        public async ValueTask<MyAppResponse<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            return await _UserService.CreateUser(request);
        }
    }
    #endregion
}

