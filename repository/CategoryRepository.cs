using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using Entities;
namespace Repository
{
  public class CategoryRepository : ICategoryRepository
  {
    private MyDbContext _dbContext;
    public CategoryRepository(MyDbContext dbContext)
    {
      _dbContext = dbContext;
    }
    public async Task<List<Category>> GetCategories()
    {
      return await _dbContext.Categories.ToListAsync();
    }
    public async Task<Category> AddNewCategory(Category category)
        {

            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();
            return category;
        }
        public async Task<Category> Delete(int id)
        {
            
            var category = await _dbContext.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.CategoryId == id);

            if (category != null)
            {
                if (category.Products != null && category.Products.Any())
                {
                    _dbContext.Products.RemoveRange(category.Products);
                }
                _dbContext.Categories.Remove(category);

                await _dbContext.SaveChangesAsync();
            }
            return category;
        }

    }
}
