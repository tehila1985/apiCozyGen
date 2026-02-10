using AutoMapper;
using Dto;
using Model;
using Repository;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
  public class ProductService : IProductService
  {
    IProductRepository _r;
    IMapper _mapper;
        public ProductService(IProductRepository i, IMapper mapper)
            {
                  _r = i;
                 _mapper= mapper;
             }

        public async Task<(IEnumerable<DtoProduct_Id_Name_Category_Price_Desc_Image>,int TotalCount)> GetProducts(int position, int skip, string? desc, int? minPrice, int? maxPrice, int?[] categoryIds)
        {
            var u = await _r.getProducts(position,skip,desc,minPrice, maxPrice,categoryIds);
            var r = _mapper.Map<List<Product>, List<DtoProduct_Id_Name_Category_Price_Desc_Image>>(u.Items);
            return (r, u.TotalCount);
        }
        public async Task<Product> AddNewProduct(Product product)
        {

        }

  }
}
