using Dto;


namespace Services
{
  public interface ICategoryService
  {
    Task<IEnumerable<DtoCategory_Name_Id>> GetCategories();
  }
}
