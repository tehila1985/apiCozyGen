using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Entities;
using System.Collections.Specialized;
using System.Reflection.PortableExecutable;
using System.Text.Json;

namespace Repository
{
    public class UserRepository : IUserRepository
    {
        private MyDbContext _dbContext;
        public UserRepository(MyDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<List<User>> GetUsers()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User?> GetUserById(int id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<User> AddNewUser(User user)
        {
           
              await _dbContext.Users.AddAsync(user);
              await _dbContext.SaveChangesAsync();
              return user;
        }

        public async Task<User?> Login(User value)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(user => user.PasswordHash == value.PasswordHash && user.Email == value.Email);
        }

        public async Task<User> Update(int id, User value)
        {
            _dbContext.Users.Update(value);
            await _dbContext.SaveChangesAsync();
            return value;
        }
        
        public async Task<User> GetUserByIdAndPassword(int id, string password)
        {
            return await _dbContext.Users
                 .FirstOrDefaultAsync(u => u.UserId == id && u.PasswordHash == password);
        }
    }
}
