namespace eStoreCA.Shared.Dtos;

public class GetByIdRoleDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<string> RolePermissions { get; set; }
}