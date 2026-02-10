
using Repository.Models;

namespace Repository
{
  public interface IProductRepository
  {
        
        Task<Product> AddNewProduct(Product product);
       Task<(List<Product> Items, int TotalCount)> getProducts(int position, int skip, string? desc, int? minPrice, int? maxPrice, int?[] categoryIds);
  }
}