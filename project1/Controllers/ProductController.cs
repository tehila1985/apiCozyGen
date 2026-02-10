using Dto;
using Microsoft.AspNetCore.Mvc;
using Model;
using Services;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ProductController : ControllerBase
  {
        ILogger<ProductController> _logger;
        IProductService _s;
    public ProductController(IProductService i, ILogger<ProductController> logger)
        {
            _s = i;
            _logger = logger;
        }
        // GET: api/<CategoryController>
    [HttpGet]
    public async Task<(IEnumerable<DtoProduct_Id_Name_Category_Price_Desc_Image>, int TotalCount)> Gets([FromBody] int position, int skip, string? desc, int? minPrice, int? maxPrice, int?[] categoryIds)
    {
      return await _s.GetProducts(position,skip,desc,minPrice,maxPrice,categoryIds);
    }
  }
}


