
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
    public class GetAllRoleWithoutClaimsQuery : IRequest<MyAppResponse<List<GetAllRoleWithoutClaimsDto>>>
    {

#region Custom Properties
#endregion Custom Properties



    }
    #endregion
    #region GetAll Query Handler
    public class GetAllRoleWithoutClaimsQueryHandler : IRequestHandler<GetAllRoleWithoutClaimsQuery, MyAppResponse<List<GetAllRoleWithoutClaimsDto>>>
    {

        private readonly IRoleService _roleService;

        public GetAllRoleWithoutClaimsQueryHandler(IRoleService roleService)
        {
            _roleService = roleService;

           
#region Custom Constructor
#endregion Custom Constructor


        }

        public async ValueTask<MyAppResponse<List<GetAllRoleWithoutClaimsDto>>> Handle(GetAllRoleWithoutClaimsQuery request, CancellationToken cancellationToken)
        {
 
#region Custom
#endregion Custom

return await _roleService.GetAllRolesWithoutClaims();

            }
    }
    #endregion
}

