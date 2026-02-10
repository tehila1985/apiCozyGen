using AutoMapper;
using Dto;
using Repository;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
  public class CategoryService : ICategoryService
  {
        ICategoryRepository _r;
        IMapper _mapper;
        IPasswordService _passwordService;
        public CategoryService(ICategoryRepository i, IMapper mapperr)
        {
            _r = i;
            _mapper = mapperr;
        }
        public async Task<IEnumerable<DtoCategory_Name_Id>> GetCategories()
            {

            var u = await _r.GetCategories();
            var r = _mapper.Map<List<Category>,List< DtoCategory_Name_Id>>(u);
            return r;
            }
  }
}
