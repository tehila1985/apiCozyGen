
using Repository.Models;

namespace Repository
{
  public interface IOrderRepository
  {
    Task<Order> AddNewOrder(Order order);
    Task<Order?> GetOrderById(int id);
  }
}