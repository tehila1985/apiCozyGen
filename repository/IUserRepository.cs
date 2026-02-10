using Repository.Models;

namespace Repository
{
    public interface IUserRepository
    {
        Task<User> AddNewUser(User user);
        void Delete(int id);
        Task<User?> GetUserById(int id);
        Task<List<User>> GetUsers();
        Task<User?> Login(User value);
        Task<User> update(int id, User value);
    }
}