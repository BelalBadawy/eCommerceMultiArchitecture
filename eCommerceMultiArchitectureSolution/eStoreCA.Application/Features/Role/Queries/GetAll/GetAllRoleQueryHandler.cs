
using Mapster;
using MapsterMapper;
using Mediator;
using eStoreCA.Application.Attributes;
using eStoreCA.Shared.Common;
using eStoreCA.Shared.Dtos;
using eStoreCA.Shared.Interfaces;


namespace eStoreCA.Application.Features.Queries

{
    #region GetAll Query Parameters
    [Authorize(Policy = AppPermissions.RolePermissions.List)]
    public class GetAllRoleQuery :  GetAllRoleQueryDto, IRequest<MyAppResponse<List<GetAllRoleDto>>>
    {
     


#region Custom Properties
#endregion Custom Properties


    }
    #endregion
    #region GetAll Query Handler
    public class GetAllRoleQueryHandler : IRequestHandler<GetAllRoleQuery, MyAppResponse<List<GetAllRoleDto>>>
    {

         private readonly IRoleService _roleService;

        public GetAllRoleQueryHandler(IRoleService roleService)
        {
            _roleService = roleService;

         
#region Custom Constructor
#endregion Custom Constructor



        }

        public async ValueTask<MyAppResponse<List<GetAllRoleDto>>> Handle(GetAllRoleQuery request, CancellationToken cancellationToken)
        {
 
#region Custom
#endregion Custom



            return await _roleService.GetAllRoles(request);
        }
    }
    #endregion
}

