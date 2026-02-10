using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    public class DtoProduct_Name_Description_Price_Stock_CategoryId_IsActive_StyleIds
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }
        public bool IsActive { get; set; } = true;
        public List<int> StyleIds { get; set; } = new List<int>();
    }
}
