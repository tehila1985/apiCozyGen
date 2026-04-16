
using DTOs;

namespace Services
{
    public interface IPasswordService
    {
        int getStrengthByPassword(string p);
    }
}