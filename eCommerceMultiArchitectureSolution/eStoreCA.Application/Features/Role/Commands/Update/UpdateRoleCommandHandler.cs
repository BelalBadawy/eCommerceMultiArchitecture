 
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
    #region Update Command Parameters
    [Authorize(Policy = AppPermissions.RolePermissions.Edit)]
    public class UpdateRoleCommand : UpdateRoleDto, IRequest<MyAppResponse<bool>>
    {
        #region Custom Properties
        #endregion Custom Properties

    }
    #endregion
    #region Update Command Handler
    public class UpdateRoleCommandHandler :  IRequestHandler<UpdateRoleCommand, MyAppResponse<bool>>
    {
               private readonly IRoleService _roleService;

        public UpdateRoleCommandHandler(IRoleService roleService)
        {
            _roleService = roleService;
            #region Custom Constructor
            #endregion Custom Constructor

        }

        public async ValueTask<MyAppResponse<bool>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            #region Custom
            #endregion Custom
          
            return await _roleService.UpdateRole(request);
        }
    }
    #endregion
}



