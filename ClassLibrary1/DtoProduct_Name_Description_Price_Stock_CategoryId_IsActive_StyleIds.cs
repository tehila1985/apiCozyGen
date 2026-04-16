
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public record DtoProduct_Name_Description_Price_Stock_CategoryId_IsActive_StyleIds(
        string Name,
        string Description,
        decimal Price,
        string FrontImageUrl,
        string BackImageUrl,
        List<DtoSyle_id_name> ProductStyles,
        int Stock,
        int CategoryId,
        bool IsActive
    );
}



