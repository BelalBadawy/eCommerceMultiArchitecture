using eStoreCA.Application.Attributes;
using eStoreCA.Shared.Common;
using eStoreCA.Shared.Dtos;
using eStoreCA.Shared.Interfaces;
using Mediator;

namespace eStoreCA.Application.Features.Commands

{
    #region Update Command Parameters
    [Authorize(Policy = AppPermissions.RolePermissions.Edit)]
    public class UpdateRoleCommand : UpdateRoleDto, IRequest<MyAppResponse<bool>>
    {

    }
    #endregion

    #region Update Command Handler
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, MyAppResponse<bool>>
    {
        private readonly IRoleService _roleService;

        public UpdateRoleCommandHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async ValueTask<MyAppResponse<bool>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            return await _roleService.UpdateRole(request);
        }
    }
    #endregion
}



