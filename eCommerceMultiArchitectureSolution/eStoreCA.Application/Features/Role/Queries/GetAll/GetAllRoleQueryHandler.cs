using eStoreCA.Application.Attributes;
using eStoreCA.Shared.Common;
using eStoreCA.Shared.Dtos;
using eStoreCA.Shared.Interfaces;
using Mediator;


namespace eStoreCA.Application.Features.Queries
{
    #region GetAll Query Parameters
    [Authorize(Policy = AppPermissions.RolePermissions.List)]
    public class GetAllRoleQuery : GetAllRoleQueryDto, IRequest<MyAppResponse<List<GetAllRoleDto>>>
    {

    }
    #endregion

    #region GetAll Query Handler
    public class GetAllRoleQueryHandler : IRequestHandler<GetAllRoleQuery, MyAppResponse<List<GetAllRoleDto>>>
    {

        private readonly IRoleService _roleService;

        public GetAllRoleQueryHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async ValueTask<MyAppResponse<List<GetAllRoleDto>>> Handle(GetAllRoleQuery request, CancellationToken cancellationToken)
        {
            return await _roleService.GetAllRoles(request);
        }
    }
    #endregion
}

