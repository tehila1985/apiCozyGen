using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
  public class ProductRepository : IProductRepository
  {
    myDBContext dbContext;
    public ProductRepository(myDBContext dbContext)
    {
      this.dbContext = dbContext;
    }
   public async Task<(List<Product> Items, int TotalCount)> getProducts([FromQuery] int position,
     [FromQuery] int skip,
     [FromQuery] string? desc,
     [FromQuery] int? minPrice,
     [FromQuery] int? maxPrice,
     [FromQuery] int?[] categoryIds)
 {
            var query = dbContext.Products.Where(product =>
                (desc == null ? (true) : (product.Description.Contains(desc)))
                && ((minPrice == null) ? (true) : (product.Price >= minPrice))
                && ((maxPrice == null) ? (true) : (product.Price <= maxPrice))
                && ((categoryIds.Length == 0) ? (true) : (categoryIds.Contains(product.CategoryId))))
                .OrderBy(product => product.Price);

            Console.WriteLine(query.ToQueryString());
            List<Product> products = await query.Skip((position - 1) * skip)
                .Take(skip).Include(product => product.Category).ToListAsync();

            var total = await query.CountAsync();

            return (products, total);
    }
        public async Task<Product> AddNewProduct(Product product)
        {

            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();
            return product;
        }
        public void Delete(int id)
        {

        }
    }
}
