using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Dto
{
    public record DtoUser_Name_Gmail(
          int UserId,
          [property: Required(ErrorMessage = "Gmail is required")]
        [property: EmailAddress(ErrorMessage = "Invalid email format.")]
        string Email,
          string FirstName,
          string LastName,
          string Role
      );
}
