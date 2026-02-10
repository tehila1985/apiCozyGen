using Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Dto
{
    public class DtoOrder_Id_UserId_Date_Sum_OrderItems
    {
        public int OrderId { get; set; }

        [Required(ErrorMessage = "User ID is required.")]
        public int UserId { get; set; }

        public DateOnly? OrderDate { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Sum must be bigger than 0")]
        public int? TotalPrice { get; set; }

        public virtual ICollection<DtoOrderItem_Id_OrderId_ProductId_Quantity> OrderItems { get; set; } = new List<DtoOrderItem_Id_OrderId_ProductId_Quantity>();
        public string Status { get; set; }
    }
}
