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
        [HttpPost]
        public async Task<ActionResult<DtoCategory_Name_Id>> Post([FromQuery] DtocategoryAll categoryDto)
        {

            DtoCategory_Name_Id res = await _s.AddNewCategory(categoryDto);
            if (res != null)
            {
                return CreatedAtAction(nameof(Get), new { id = res.CategoryId }, res);
            }
            else
                return BadRequest();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<DtoCategory_Name_Id>> Delete(int id)
        {
            DtoCategory_Name_Id res = await _s.Delete(id);

            if (res != null)
            {
                return Ok(res);
            }
            else
            {
                return NotFound($"Category with ID {id} not found");
            }
        }
    }
}
