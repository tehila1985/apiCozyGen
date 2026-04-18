
using DTOs;

namespace Services
{
    public interface IPasswordService
    {
        int GetStrengthByPassword(string p);
    }
}