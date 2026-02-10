using AutoMapper;
using Dto;
using Model;
namespace Api
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<User, DtoUser_Gmail_Password>().ReverseMap();
            CreateMap<User, DtoUser_Id_Name>().ReverseMap();
            CreateMap<PassWord, DtoPassword_Password_Strength>().ReverseMap();
            CreateMap<User, DtoUser_Name_Password_Gmail>().ReverseMap();
            CreateMap<User, DtoUser_Name_Gmail>().ReverseMap();
            CreateMap<Category, DtoCategory_Name_Id>().ReverseMap();
            //CreateMap<Product, DtoProduct_Id_Name_Category_Price_Desc_Image>().ForMember(dest => dest.Name,
            //                                              opts => opts.MapFrom(src => src.Category.Name));
            CreateMap<Product, DtoProduct_Id_Name_Category_Price_Desc_Image>()
                           .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<Order, DtoOrder_Id_UserId_Date_Sum_OrderItems>()
                .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems)).ReverseMap();
            CreateMap<OrderItem, DtoOrderItem_Id_OrderId_ProductId_Quantity>()
                 .ForMember(dest => dest.ItemName, opt => opt.MapFrom(src => src.Product.Name));
        }
    }
}