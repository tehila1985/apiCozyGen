using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

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

        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public virtual ICollection<DtoProductStyle> ProductStyles { get; set; } = new List<DtoProductStyle>();

    }
}
