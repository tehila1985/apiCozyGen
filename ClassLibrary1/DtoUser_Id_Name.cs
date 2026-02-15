using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Dto
{
    //public record DtoUser_Id_Name(string? UserId,string FirstName);

    public class DtoUser_Id_Name
    {
        public string? UserId { get; set; }
        public string FirstName { get; set; }
    }


}
