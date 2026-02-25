using Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    public class DtoProduct_Id_Name_Category_Price_Desc_Image
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product name is required")]
        public string Name { get; set; }

        public int? CategoryId { get; set; } 

        public string CategoryName { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Price must be bigger than 0")]
        public int? Price { get; set; }
        public int Stock { get; set; }

        public string Description { get; set; }

        [StringLength(500)]
        public string FrontImageUrl { get; set; }

        [StringLength(500)]
        public string BackImageUrl { get; set; }



        public virtual ICollection<DtoProductStyle> ProductStyles { get; set; } = new List<DtoProductStyle>();

    }
}

//public record DtoProduct_Id_Name_Category_Price_Desc_Image(
//       int ProductId,

//       [property: Required(ErrorMessage = "Product name is required")]
//        string Name,
//       int? CategoryId,
//       string CategoryName,
//       [property: Range(0, int.MaxValue, ErrorMessage = "Price must be bigger than 0")]
//        int? Price,
//       string Description,
//       [property: StringLength(500)]
//        string FrontImageUrl,
//       [property: StringLength(500)]
//        string BackImageUrl,
//       ICollection<DtoProductStyle> ProductStyles
//   );