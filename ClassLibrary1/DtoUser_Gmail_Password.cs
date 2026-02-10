using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Dto
{
    //public record DtoUser_Gmail_Password(string Gmail,string Password);
    public class DtoUser_Gmail_Password
    {

        [Required(ErrorMessage = "Gmail is required")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string PasswordHash { get; set; }
    }
}

