using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Dto
{
    public record DtoUser_Id_Name(
        string? UserId,
        string FirstName,
        string Role
    );
}
