


using Mapster;
using MapsterMapper;
using Mediator;
using eStoreCA.Application.Attributes;
using eStoreCA.Shared.Common;
using eStoreCA.Shared.Dtos;
using eStoreCA.Shared.Interfaces;

namespace eStoreCA.Application.Features.Commands

{

 #region Create Command Parameters
    [Authorize(Policy = AppPermissions.RolePermissions.Create)]
    public class CreateRoleCommand : CreateRoleDto, IRequest<MyAppResponse<Guid>>
    {

    }
    #endregion
    #region Create Command Handler
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, MyAppResponse<Guid>>
    {
               private readonly IRoleService _roleService;
        public CreateRoleCommandHandler(IRoleService roleService)
        {
            _roleService = roleService;

            #region Custom Constructor
            #endregion Custom Constructor

        }
        public async ValueTask<MyAppResponse<Guid>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            #region Custom
            #endregion Custom
          

            return await _roleService.CreateRole(request);
        }
    }
    #endregion
}

