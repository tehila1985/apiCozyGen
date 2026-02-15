using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Dto
{
    
    public class DtoUser_Name_Gmail
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Gmail is required")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
       
    }

}


//public record DtoUser_Name_Gmail(
//    int UserId,
//    [property: Required(ErrorMessage = "Gmail is required")]
//    [property: EmailAddress(ErrorMessage = "Invalid email format.")]
//    string Email,
//    string FirstName,
//    string LastName
//);
