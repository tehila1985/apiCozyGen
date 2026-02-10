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
    }
}
