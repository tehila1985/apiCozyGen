using Dto;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Collections.Generic;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _s;
        private readonly IUserServices _userService; 

        public ProductController(IProductService i, ILogger<ProductController> logger, IUserServices userService)
        {
            _s = i;
            _logger = logger;
            _userService = userService; 
        }

        [HttpGet]
        public async Task<Dto_result_product> Gets(
            [FromQuery] int position,
            [FromQuery] int skip,
            [FromQuery] string? desc,
            [FromQuery] int? minPrice,
            [FromQuery] int? maxPrice,
            [FromQuery] int?[] categoryIds,
            [FromQuery] int?[] styleIds)
        {
            return await _s.GetProducts(position, skip, desc, minPrice, maxPrice, categoryIds, styleIds);
        }

        [HttpPost]
        public async Task<ActionResult<DtoProduct_Id_Name_Category_Price_Desc_Image>> Post(
            [FromBody] DtoProduct_Name_Description_Price_Stock_CategoryId_IsActive_StyleIds productDto, 
            [FromHeader] int userId,  
            [FromHeader] string password)
        {
            
            bool isAdmin = await _userService.IsAdminById(userId, password);
            if (!isAdmin)
            {
                return Forbid("גישה נדחתה: דרושות הרשאות מנהל לביצוע פעולה זו");
            }

            DtoProduct_Id_Name_Category_Price_Desc_Image res = await _s.AddNewProduct(productDto);
            if (res != null)
            {
                return Ok(res);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DtoProduct_Id_Name_Category_Price_Desc_Image>> Delete(
            int id,
            [FromHeader] int userId,   
            [FromHeader] string password)
        {
           
            bool isAdmin = await _userService.IsAdminById(userId, password);
            if (!isAdmin)
            {
                return Forbid("גישה נדחתה: דרושות הרשאות מנהל למחיקת מוצר");
            }

            DtoProduct_Id_Name_Category_Price_Desc_Image res = await _s.Delete(id);

            if (res != null)
            {
                return Ok(res);
            }
            return NotFound($"Product with ID {id} not found");
        }
    }
}