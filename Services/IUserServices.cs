using Dto;
using Repository.Models;

namespace Services
{
    public interface IUserServices
    {
        Task<DtoUser_Id_Name> AddNewUser(DtoUser_Name_Password_Gmail user);
        void Delete(int id);
        Task<DtoUser_Name_Gmail?> GetUserById(int id);
        Task<IEnumerable<User>> GetUsers();
        Task<DtoUser_Id_Name?> Login(DtoUser_Gmail_Password value);
        Task<DtoUser_Id_Name> update(int id, DtoUser_Name_Password_Gmail value);
        Task<bool> IsAdminById(int id, string password);
    }
}
