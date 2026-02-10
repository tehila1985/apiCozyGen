using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using Repository.Models;
namespace Repository
{
  public class CategoryRepository : ICategoryRepository
  {
    myDBContext dbContext;
    public CategoryRepository(myDBContext dbContext)
    {
      this.dbContext = dbContext;
    }
    public async Task<List<Category>> GetCategories()
    {
      return await dbContext.Categories.ToListAsync();
    }
  }
}
