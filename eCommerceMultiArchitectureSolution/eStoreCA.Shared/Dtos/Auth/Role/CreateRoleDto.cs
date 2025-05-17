using System.ComponentModel.DataAnnotations;
namespace eStoreCA.Shared.Dtos
{
    public class CreateRoleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<string> RolePermissions { get; set; }

        #region Custom
        #endregion Custom

    }
}
