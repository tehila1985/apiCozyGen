using System.ComponentModel.DataAnnotations;

namespace Dto
{
    public record DtoUser_Name_Password_Gmail(
    int UserId,
    [property: Required(ErrorMessage = "Gmail is required")]
        [property: EmailAddress(ErrorMessage = "Invalid email")]
        string Email,
    string FirstName,
    string LastName,
    [property: Required(ErrorMessage = "Password is required")]
        string PasswordHash,
    [property: StringLength(50)]
        string Phone,
    [property: StringLength(255)]
        string Address
);
}