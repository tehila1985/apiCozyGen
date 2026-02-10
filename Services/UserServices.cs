using System.Text.Json;
using Repository;
using AutoMapper;
using Dto;
using Repository.Models;
namespace Services
{


    public class UserServices : IUserServices
    {
        IUserRepository _r;
        IMapper _mapper;
        IPasswordService _passwordService;
        public UserServices(IUserRepository i, IMapper mapperr, IPasswordService passwordService)
        {
            _r = i;
            _mapper = mapperr;
            _passwordService = passwordService;
        }
        public  async Task<IEnumerable<User>> GetUsers()
        {
            return await _r.GetUsers();
        }
        public async Task<DtoUser_Name_Gmail?> GetUserById(int id)
        {
            var u = await _r.GetUserById(id);
            var r= _mapper.Map<User, DtoUser_Name_Gmail>(u);
            return r;
        }

        public async Task<DtoUser_Id_Name> AddNewUser(DtoUser_Name_Password_Gmail user)
        {

            int d = _passwordService.getStrengthByPassword(user.PasswordHash);
            if (d >= 2)
            {
                var a =_mapper.Map<DtoUser_Name_Password_Gmail, User>(user);

                var res = await _r.AddNewUser(a);
                var DtoUser = _mapper.Map<User, DtoUser_Id_Name>( res);
                return DtoUser;
            }

            return null;
        }

        public async Task<DtoUser_Id_Name?> Login(DtoUser_Gmail_Password value)
        {
            var a= _mapper.Map<DtoUser_Gmail_Password, User>(value);
            var u = _r.Login(a);

            var DtoUser = _mapper.Map<User, DtoUser_Id_Name>(await u);
        }
        public async Task<DtoUser_Id_Name> update(int id, DtoUser_Name_Password_Gmail user)
        {
          
            DtoPassword_Password_Strength d = _passwordService.getStrengthByPassword(user.Password);
            if (d.Strength >= 2)
            {
                var a = _mapper.Map<DtoUser_Name_Password_Gmail, User>(user);
                var res = await _r.update(id,a);
                var DtoUser = _mapper.Map<User, DtoUser_Id_Name>(res);
                return DtoUser;
            }
            return null;
        }
        public void Delete(int id)
        {
            _r.Delete(id);
        }

    }
}
