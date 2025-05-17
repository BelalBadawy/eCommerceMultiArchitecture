

using Mapster;
using MapsterMapper;
using Mediator;
using Microsoft.EntityFrameworkCore;
using eStoreCA.Application.Attributes;
using eStoreCA.Domain.Entities;
using eStoreCA.Application.Interfaces;
using eStoreCA.Shared.Common;
using eStoreCA.Shared.Dtos;
using eStoreCA.Shared.Interfaces;

namespace eStoreCA.Application.Features.Commands

{
                    #region Create Command Parameters
                    [Authorize(Policy = AppPermissions.RolePermissions.Delete)]
        public class DeleteRoleCommand : DeleteRoleDto,IRequest<MyAppResponse<bool>>
    {
  
        #region Custom Properties
        #endregion Custom Properties

    }
    #endregion
    #region Delete Command Handler
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, MyAppResponse<bool>>
    {
    private readonly IRoleService _roleService;

    public DeleteRoleCommandHandler(IRoleService roleService)
    {
        _roleService = roleService;

        #region Custom Constructor
        #endregion Custom Constructor

    }

    public async ValueTask<MyAppResponse<bool>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        #region Custom
        #endregion Custom

        return await _roleService.DeleteRole(request);


    }
    }
    #endregion
    }

          



