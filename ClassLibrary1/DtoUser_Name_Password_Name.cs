using System.ComponentModel.DataAnnotations;

namespace Dto
{
    public class DtoUser_Name_Password_Gmail
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Gmail is required")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string PasswordHash { get; set; }
        [StringLength(50)]
        public string Phone { get; set; }

        [StringLength(255)]
        public string Address { get; set; }
    }
}



//public record DtoUser_Name_Password_Gmail(
//    int UserId,
//    [property: Required(ErrorMessage = "Gmail is required")]
//    [property: EmailAddress(ErrorMessage = "Invalid email")]
//    string Email,
//    string FirstName,
//    string LastName,
//    [property: Required(ErrorMessage = "Password is required")]
//    string PasswordHash
//);