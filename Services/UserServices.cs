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
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _r.GetUsers();
        }
        public async Task<DtoUser_Name_Gmail?> GetUserById(int id)
        {
            var u = await _r.GetUserById(id);
            var r = _mapper.Map<User, DtoUser_Name_Gmail>(u);
            return r;
        }
        public async Task<DtoUser_Id_Name> AddNewUser(DtoUser_Name_Password_Gmail user)
        {
            int d = _passwordService.getStrengthByPassword(user.PasswordHash);
            if (d >= 2)
            {
                
                var userEntity = _mapper.Map<DtoUser_Name_Password_Gmail, User>(user);
                userEntity.Role = "Customer";
                var res = await _r.AddNewUser(userEntity);
                var dtoUser = _mapper.Map<User, DtoUser_Id_Name>(res);
                return dtoUser;
            }

            return null;
        }

        public async Task<DtoUser_Id_Name?> Login(DtoUser_Gmail_Password value)
        {
            var a = _mapper.Map<DtoUser_Gmail_Password, User>(value);
            var u = await _r.Login(a);

            if (u == null) return null;

            var dtoUser = _mapper.Map<User, DtoUser_Id_Name>(u);
            return dtoUser;
        }
        //public async Task<DtoUser_Id_Name> update(int id, DtoUser_Name_Password_Gmail user)
        //{

        //    int d = _passwordService.getStrengthByPassword(user.PasswordHash);
        //    if (d >= 2)
        //    {
        //        var a = _mapper.Map<DtoUser_Name_Password_Gmail, User>(user);
        //        var res = await _r.update(id, a);
        //        var DtoUser = _mapper.Map<User, DtoUser_Id_Name>(res);
        //        return DtoUser;
        //    }
        //    return null;
        //}
        public async Task<DtoUser_Id_Name> update(int id, DtoUser_Name_Password_Gmail userDto)
        {
            // 1. בדיקת חוזק סיסמה
            int d = _passwordService.getStrengthByPassword(userDto.PasswordHash);
            if (d < 2) return null;

            // 2. שליפת המשתמש המלא מהמסד (כדי לשמור על ה-Role ושדות אחרים שלא ב-DTO)
            var existingUser = await _r.GetUserById(id);
            if (existingUser == null) return null;

            // 3. שימוש ב-AutoMapper לעדכון האובייקט הקיים
            // הדרך הזו מעתיקה את FirstName, LastName, Email ו-PasswordHash 
            // מה-DTO לתוך ה-existingUser באופן אוטומטי.
            _mapper.Map(userDto, existingUser);

            // 4. חשוב: בגלל שה-DTO מכיל UserId, המאפר עלול לנסות לעדכן אותו. 
            // כדי להיות בטוחות שהמפתח לא משתנה (מה שגרם לשגיאה קודם), נקבע אותו שוב:
            existingUser.UserId = id;

            // 5. שמירה (ה-Role המקורי נשאר כי הוא לא נדרס במיפוי)
            var res = await _r.update(id, existingUser);

            return _mapper.Map<User, DtoUser_Id_Name>(res);
        }
        public void Delete(int id)
        {
            _r.Delete(id);
        }

    }
}