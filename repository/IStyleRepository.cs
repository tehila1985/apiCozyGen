using Repository.Models;

namespace Repository
{
    public interface IStyleRepository
    {
        Task<List<Style>> GetStyles();
    }
}