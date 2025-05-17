

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using eStoreCA.Shared.Common;
using eStoreCA.Shared.Dtos;
using eStoreCA.Shared.Interfaces;
using System.Security.Claims;
using eStoreCA.Domain.Entities;

namespace eStoreCA.Infrastructure.Identity.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        public RoleService(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;

        }

        public async Task<MyAppResponse<Guid>> CreateRole(CreateRoleDto request)
        {
            var newRoleName = request.Name.Trim();

            var existRole = await _roleManager.FindByNameAsync(newRoleName);

            if (existRole != null)
            {
return new MyAppResponse<Guid>(string.Format(SD.ExistData, request.Name));
}

                       
                await _roleManager.CreateAsync(new ApplicationRole() { Name = newRoleName});

                var newRole = await _roleManager.FindByNameAsync(newRoleName);

                if (newRole != null)
                {
                    if (request.RolePermissions != null && request.RolePermissions.Any())
                    {
                        var claims = await _roleManager.GetClaimsAsync(newRole);
                        var selectedClaims = request.RolePermissions;
                        foreach (var p in selectedClaims)
                        {
                            if (!string.IsNullOrEmpty(p))
                            {
                                await _roleManager.AddClaimAsync(newRole,
                                    new Claim(CustomClaimTypes.Permission, p.Trim().ToUpper()));
                            }
                        }
                    }
                }
                return new MyAppResponse<Guid>(data: newRole.Id);
            

            return new MyAppResponse<Guid>("Error in saving data");
        }

        public async Task<MyAppResponse<bool>> DeleteRole(DeleteRoleDto request)
        {

var result = await _roleManager.FindByIdAsync(request.Id.ToString());


        

            if (result != null)
            {
                await _roleManager.DeleteAsync(result);

                return new MyAppResponse<bool>(true);
            }

            return new MyAppResponse<bool>(false);
        }

        public async Task<MyAppResponse<List<GetAllRoleDto>>> GetAllRoles(GetAllRoleQueryDto request)
        {
            List<GetAllRoleDto> dtos = new List<GetAllRoleDto>();

            List<ApplicationRole> result = null;

result = await _roleManager.Roles.ToListAsync();



            if (result != null && result.Any())
            {
                if (!string.IsNullOrEmpty(request.Search))
                {
                    result = result.Where(o => o.Name.Contains(request.Search)).ToList();
                }

                if (!string.IsNullOrEmpty(request.Sort))
                {
                    switch (request.Sort.ToLower())
                    {

                        case "id":
                            if (request.AscendingOrder)
                            {
                                result = result.OrderBy(o => o.Id).ToList();
                            }
                            else
                            {
                                result = result.OrderByDescending(o => o.Id).ToList();
                            }
                            break;

                        case "name":
                            if (request.AscendingOrder)
                            {
                                result = result.OrderBy(o => o.Name).ToList();
                            }
                            else
                            {
                                result = result.OrderByDescending(o => o.Name).ToList();
                            }

                            break;

                          

                    }
                }

                foreach (var r in result)
                {
                    GetAllRoleDto dto = new GetAllRoleDto();
                    dto.Id = r.Id;
                    dto.Name = r.Name;

                    var claims = await _roleManager.GetClaimsAsync(r);
                    if (claims != null && claims.Count > 0)
                    {
                        dto.RolePermissions = claims.Select(o => o.Value).ToList();
                    }

                    dtos.Add(dto);
                }
            }

            return new MyAppResponse<List<GetAllRoleDto>>(dtos);
        }



        public async Task<MyAppResponse<GetByIdRoleDto>> GetRoleById(GetByIdRoleQueryDto request)
        {
            GetByIdRoleDto dto = null;

            ApplicationRole result = null;

           
result = await _roleManager.FindByIdAsync(request.Id.ToString());


            if (result != null)
            {
                dto = new GetByIdRoleDto();
                dto.Id = result.Id;
                dto.Name = result.Name;

                var claims = await _roleManager.GetClaimsAsync(result);
                if (claims != null && claims.Count > 0)
                {
                    dto.RolePermissions = claims.Select(o => o.Value).ToList();
                }
            }

            return new MyAppResponse<GetByIdRoleDto>(dto);
        }

        public async Task<MyAppResponse<bool>> UpdateRole(UpdateRoleDto request)
        {
            var newRoleName = request.Name.Trim();

  
var existRole = await _roleManager.FindByNameAsync(newRoleName);


            

            if (existRole != null && existRole.Id != request.Id)
            {
                return new MyAppResponse<bool>(string.Format(SD.ExistData, newRoleName));
            }

           
var roleToUpdate = await _roleManager.FindByIdAsync(request.Id.ToString());


            roleToUpdate.Name = newRoleName;

            if (roleToUpdate != null)
            {
                await _roleManager.UpdateAsync(roleToUpdate);


                var claims = await _roleManager.GetClaimsAsync(roleToUpdate);

                foreach (var c in claims)
                {
                    await _roleManager.RemoveClaimAsync(roleToUpdate, c);
                }
                 if (request.RolePermissions != null && request.RolePermissions.Any())
                {
                    var selectedClaims = request.RolePermissions;
                    foreach (var a in selectedClaims)
                    {
                        if (!string.IsNullOrEmpty(a))
                        {
                            await _roleManager.AddClaimAsync(roleToUpdate,
                                new Claim(CustomClaimTypes.Permission, a.Trim().ToUpper()));
                        }
                    }
                }
                return new MyAppResponse<bool>(true);
            }
            return new MyAppResponse<bool>(false);
        }


public async Task<MyAppResponse<List<GetAllRoleWithoutClaimsDto>>> GetAllRolesWithoutClaims()

        
        {

            List<GetAllRoleWithoutClaimsDto> dtos = new List<GetAllRoleWithoutClaimsDto>();

            List<ApplicationRole> result = null;


result = await _roleManager.Roles.ToListAsync();

            


            if (result != null && result.Any())
            {
                return new MyAppResponse<List<GetAllRoleWithoutClaimsDto>>(result.Select(o => new GetAllRoleWithoutClaimsDto() { Id = o.Id, Name = o.Name }).ToList());
            }

            return new MyAppResponse<List<GetAllRoleWithoutClaimsDto>>(dtos);
        }
    }
}

