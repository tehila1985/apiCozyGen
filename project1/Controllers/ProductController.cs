using Dto;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<(IEnumerable<DtoProduct_Id_Name_Category_Price_Desc_Image>, int TotalCount)> Gets([FromBody] int position, int skip, string? desc, int? minPrice, int? maxPrice, [FromQuery] int?[] categoryIds)
        {
          return await _s.GetProducts(position,skip,desc,minPrice,maxPrice,categoryIds);
        }

        // POST api/<users>

        [HttpPost]
        public async Task<ActionResult<DtoProduct_Id_Name_Category_Price_Desc_Image>> Post([FromBody] DtoProduct_Name_Description_Price_Stock_CategoryId_IsActive_StyleIds productDto)
        {

            DtoProduct_Id_Name_Category_Price_Desc_Image res = await _s.AddNewProduct(productDto);
            if (res != null)
            {
                return CreatedAtAction(nameof(Gets), new { id = res.ProductId }, res);
            }
            else
                return BadRequest();
        }
    }
}


