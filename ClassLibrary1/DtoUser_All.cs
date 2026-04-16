using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public record DtoUser_All(
        int UserId,
        string Email,
        string FirstName,
        string LastName,
        string PasswordHash,
        string Phone,
        string Address
    );
}
