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
    public class DtoProduct_Name_Description_Price_Stock_CategoryId_IsActive_StyleIds
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }
        public bool IsActive { get; set; } = true;
        [StringLength(500)]
        public string FrontImageUrl { get; set; }

        [StringLength(500)]
        public string BackImageUrl { get; set; }
        public List<DtoSyle_id_name> ProductStyles { get; set; } = new List<DtoSyle_id_name>();
    }
}




//public record DtoProduct_Name_Description_Price_Stock_CategoryId_IsActive_StyleIds(
//      [property: Required, StringLength(200)]
//        string Name,
//      [property: StringLength(1000)]
//        string Description,
//      [property: Column(TypeName = "decimal(10, 2)")]
//           decimal Price,
//      int Stock,
//      int CategoryId,
//      [property: StringLength(500)]
//            string FrontImageUrl,
//      [property: StringLength(500)]
//        string BackImageUrl,
//      bool IsActive = true,
//      List<DtoSyle_id_name>? ProductStyles = null
//  );



