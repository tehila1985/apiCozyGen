using Entities;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RatingRepository : IRatingRepository
    {
        private readonly myDBContext _apiDbContext;
        public RatingRepository(myDBContext apiDbContext)
        {
            _apiDbContext = apiDbContext;
        }
        public async Task<Rating> AddRating(Rating newRating)
        {
            await _apiDbContext.Ratings.AddAsync(newRating);
            await _apiDbContext.SaveChangesAsync();
            return newRating;
        }
    }
}
