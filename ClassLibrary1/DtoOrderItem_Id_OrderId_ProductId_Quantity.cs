using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    public record DtoOrderItem_Id_OrderId_ProductId_Quantity(
        int OrderItemId,
        int OrderId,
        string ItemName,
        int? ProductId,
        int Quantity,
        decimal PriceAtPurchase
    );
}