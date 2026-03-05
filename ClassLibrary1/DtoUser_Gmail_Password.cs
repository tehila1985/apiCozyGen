using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Dto
{
    public record DtoUser_Gmail_Password(
     [property: Required(ErrorMessage = "Gmail is required")]
    [property: EmailAddress(ErrorMessage = "Invalid email format.")]
    string Email,
     [property: Required(ErrorMessage = "Password is required")]
    string PasswordHash
 );
}
