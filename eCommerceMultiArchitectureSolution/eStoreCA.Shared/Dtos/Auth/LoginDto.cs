
using System.ComponentModel.DataAnnotations;
namespace eStoreCA.Shared.Dtos
{
 public class LoginDto
    {
        //[Required]
        //[EmailAddress]
        //[MaxLength(100)]
        public string Email { get; set; }

        //[Required]
        //[MaxLength(20)]
        public string Password { get; set; }
   
        
        public bool RememberMe { get; set; }

            
public string TenantName { get; set; }

#region Custom
#endregion Custom


}
}
