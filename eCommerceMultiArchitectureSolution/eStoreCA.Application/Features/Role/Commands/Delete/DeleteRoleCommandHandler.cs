using eStoreCA.Application.Attributes;
using eStoreCA.Shared.Common;
using eStoreCA.Shared.Dtos;
using eStoreCA.Shared.Interfaces;
using Mediator;

namespace eStoreCA.Application.Features.Commands
{
    #region Create Command Parameters
    [Authorize(Policy = AppPermissions.RolePermissions.Delete)]
    public class DeleteRoleCommand : DeleteRoleDto, IRequest<MyAppResponse<bool>>
    {

    }
    #endregion

    #region Delete Command Handler
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, MyAppResponse<bool>>
    {
        private readonly IRoleService _roleService;

        public DeleteRoleCommandHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async ValueTask<MyAppResponse<bool>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            return await _roleService.DeleteRole(request);
        }
    }
    #endregion
}





