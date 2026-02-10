using Dto;


namespace Services
{
    public interface IProductService
    {
        Task<DtoProduct_Id_Name_Category_Price_Desc_Image> AddNewProduct(DtoProduct_Name_Description_Price_Stock_CategoryId_IsActive_StyleIds productDto);
        Task<(IEnumerable<DtoProduct_Id_Name_Category_Price_Desc_Image>, int TotalCount)> GetProducts(int position, int skip, string? desc, int? minPrice, int? maxPrice, int?[] categoryIds);
    }
}