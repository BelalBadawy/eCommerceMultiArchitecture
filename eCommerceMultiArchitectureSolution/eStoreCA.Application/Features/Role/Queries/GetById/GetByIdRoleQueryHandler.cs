
using Mapster;
using MapsterMapper;
using Mediator;
using eStoreCA.Application.Attributes;
using eStoreCA.Shared.Common;
using eStoreCA.Shared.Dtos;
using eStoreCA.Shared.Interfaces;

namespace eStoreCA.Application.Features.Queries

{
    #region GetById Query Parameters
    [Authorize(Policy = AppPermissions.RolePermissions.View)]
    public class GetByIdRoleQuery : GetByIdRoleQueryDto, IRequest<MyAppResponse<GetByIdRoleDto>>
    {
       

#region Custom Properties
#endregion Custom Properties


    }
    #endregion
    #region GetById Query Handler
    public class GetByIdRoleQueryHandler : IRequestHandler<GetByIdRoleQuery, MyAppResponse<GetByIdRoleDto>>
    {
   
        private readonly IRoleService _roleService;

        public GetByIdRoleQueryHandler(IRoleService roleService)
        {
            _roleService = roleService;

#region Custom Constructor
#endregion Custom Constructor


        }
        public async ValueTask<MyAppResponse<GetByIdRoleDto>> Handle(GetByIdRoleQuery request, CancellationToken cancellationToken)
        {
           
#region Custom
#endregion Custom


       
            return await _roleService.GetRoleById(request);

        }
    }
    #endregion
}


