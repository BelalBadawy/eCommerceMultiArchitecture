namespace eStoreCA.Shared.Dtos;

public class UpdateRoleDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<string> RolePermissions { get; set; }
}