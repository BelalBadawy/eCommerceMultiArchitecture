using eStoreCA.Shared.Common;
using eStoreCA.Shared.Dtos;

namespace eStoreCA.Shared.Interfaces;

public interface IRoleService
{
    Task<MyAppResponse<Guid>> CreateRole(CreateRoleDto request);
    Task<MyAppResponse<bool>> UpdateRole(UpdateRoleDto request);
    Task<MyAppResponse<bool>> DeleteRole(DeleteRoleDto request);
    Task<MyAppResponse<List<GetAllRoleDto>>> GetAllRoles(GetAllRoleQueryDto request);
    Task<MyAppResponse<GetByIdRoleDto>> GetRoleById(GetByIdRoleQueryDto request);
    Task<MyAppResponse<List<GetAllRoleWithoutClaimsDto>>> GetAllRolesWithoutClaims(Guid tenantId);
}