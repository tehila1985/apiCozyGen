
using Repository.Models;

namespace Repository
{
  public interface ICategoryRepository
  {
    Task<List<Category>> GetCategories();
  }
}
