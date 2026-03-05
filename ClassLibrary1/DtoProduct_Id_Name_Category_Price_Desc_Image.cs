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
    public record DtoProduct_Id_Name_Category_Price_Desc_Image(
           int ProductId,
           [property: Required(ErrorMessage = "Product name is required")]
            string Name,
           int? CategoryId,
           string CategoryName,
           [property: Range(0, int.MaxValue, ErrorMessage = "Price must be bigger than 0")]
            int? Price,
           int Stock,
           string Description,
           [property: StringLength(500)]
            string FrontImageUrl,
           [property: StringLength(500)]
            string BackImageUrl,
           ICollection<DtoProductStyle> ProductStyles
       );
}