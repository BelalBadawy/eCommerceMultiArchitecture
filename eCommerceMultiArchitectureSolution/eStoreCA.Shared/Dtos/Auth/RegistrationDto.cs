using System.ComponentModel.DataAnnotations;

namespace eStoreCA.Shared.Dtos;

public class RegistrationDto
{
    [Required] public string FullName { get; set; }

    [Required] [EmailAddress] public string Email { get; set; }

    //[Required]
    //[MinLength(6)]
    //public string UserName { get; set; }

    [Required]
    [MinLength(6)]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required]
    [MinLength(6)]
    [Compare("Password")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }
}