using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{

    public record Dto_result_product(
        IEnumerable<DtoProduct_Id_Name_Category_Price_Desc_Image> Products,
        int TotalCount
    );

}