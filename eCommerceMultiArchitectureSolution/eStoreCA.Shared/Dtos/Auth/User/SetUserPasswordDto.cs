
using System.ComponentModel.DataAnnotations;
namespace eStoreCA.Shared.Dtos
{
    public class SetUserPasswordDto
    {
        public Guid Id { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }



        #region Custom
        #endregion Custom


    }
}
