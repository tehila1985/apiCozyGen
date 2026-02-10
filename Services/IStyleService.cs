using Dto;

namespace Services
{
    public interface IStyleService
    {
        Task<IEnumerable<DtoSyle_id_name>> GetStyles();
    }
}