using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace Dto
{
    public class DtoOrderItem_Id_OrderId_ProductId_Quantity
    {
        public int OrderItemId { get; set; }

        [Required(ErrorMessage = "Order ID is required")]
        public int OrderId { get; set; }

        public string ItemName { get; set; }

        [Required(ErrorMessage = "Product ID is required")]
        public int? ProductId { get; set; } 

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int? Quantity { get; set; }
    }
}
