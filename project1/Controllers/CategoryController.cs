using Dto;
using Microsoft.AspNetCore.Mvc;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CategoryController : ControllerBase
  {

      ILogger<CategoryController> _logger;
      ICategoryService _s;
    public CategoryController(ICategoryService i, ILogger<CategoryController> logger){
         _s = i;
        _logger= logger;
     }
    // GET: api/<CategoryController>
    [HttpGet]
    public async Task<IEnumerable<DtoCategory_Name_Id>> Get()
    {
      return await _s.GetCategories();
    }
  }
}
