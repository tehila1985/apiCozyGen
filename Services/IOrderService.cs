using Dto;

namespace Services
{
    public interface IOrderService
    {
        Task<DtoOrder_Id_UserId_Date_Sum_OrderItems> AddNewOrder(DtoOrder_Id_UserId_Date_Sum_OrderItems order);
        Task<DtoOrder_Id_UserId_Date_Sum_OrderItems?> GetOrderById(int id);
    }
}