using System.ComponentModel.DataAnnotations;
namespace eStoreCA.Shared.Dtos.Country
{
    public class CreateCountryDto
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Code is required")]
        [StringLength(10, ErrorMessage = "Code cannot exceed 10 characters")]
        public string Code { get; set; }
        public bool IsActive { get; set; } = true;
        #region Custom
        #endregion Custom
    }
}