using Microsoft.EntityFrameworkCore;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class StyleRepository : IStyleRepository
    {
        private MyDbContext _dbContext;
        public StyleRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Style>> GetStyles()
        {
            return await _dbContext.Styles.ToListAsync();
        }

        public async Task<Style> AddNewStyle(Style style)
        {

            await _dbContext.Styles.AddAsync(style);
            await _dbContext.SaveChangesAsync();
            return style;
        }
        public async Task<Style> Delete(int id)
        {
            var style = await _dbContext.Styles
                .Include(p => p.ProductStyles)
                .FirstOrDefaultAsync(p => p.StyleId == id);

            if (style != null)
            {
                _dbContext.ProductStyles.RemoveRange(style.ProductStyles);
                _dbContext.Styles.Remove(style);
                await _dbContext.SaveChangesAsync();
            }
            return style;
        }

      

    }
}
