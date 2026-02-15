using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    public class DtoSyle_id_name
    {
        public int StyleId { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(500)]
        public string ImageUrl { get; set; }
    }
}
//public record DtoSyle_id_name(
//    int StyleId,
//    [property: Required]
//    [property: StringLength(150)]
//    string Name,
//    [property: StringLength(500)]
//    string Description,
//    [property: StringLength(500)]
//    string ImageUrl
//);