
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public record DtoProduct_Id_Name_Category_Price_Desc_Image(
        int ProductId,
        string Name,
        int? CategoryId,
        string CategoryName,
        int? Price,
        int Stock,
        string Description,
        string FrontImageUrl,
        string BackImageUrl,
        ICollection<DtoProductStyle> ProductStyles
    );
}
