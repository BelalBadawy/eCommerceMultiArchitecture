
using System.ComponentModel.DataAnnotations;
namespace eStoreCA.Shared.Dtos
{
    public class ResetPasswordDto
    {
        public string Token { get; set; }

        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [MinLength(6)]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }





        #region Custom
        #endregion Custom


    }
}
