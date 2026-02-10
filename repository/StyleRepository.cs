using Microsoft.EntityFrameworkCore;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class StyleRepository : IStyleRepository
    {
        myDBContext dbContext;
        public StyleRepository(myDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<Style>> GetStyles()
        {
            return await dbContext.Styles.ToListAsync();
        }
    }
}
