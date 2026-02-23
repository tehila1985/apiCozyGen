using Dto;
using Microsoft.AspNetCore.Mvc;
using Services;
namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StyleController : ControllerBase
    {
        ILogger<StyleController> _logger;
        IStyleService _s;
        public StyleController(IStyleService i, ILogger<StyleController> logger)
        {
            _s = i;
            _logger = logger;
        }
        // GET: api/<CategoryController>
        [HttpGet]
        public async Task<IEnumerable<DtoSyle_id_name>> Get()
        {
            return await _s.GetStyles();
        }
        [HttpPost]
        public async Task<ActionResult<DtoSyle_id_name>> Post([FromQuery] DtoStyleAll StyleDto)
        {

            DtoSyle_id_name res = await _s.AddNewStyle(StyleDto);
            if (res != null)
            {
                return CreatedAtAction(nameof(Get), new { id = res.StyleId }, res);
            }
            else
                return BadRequest();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<DtoSyle_id_name>> Delete(int id)
        {
            DtoSyle_id_name res = await _s.Delete(id);

            if (res != null)
            {
                return Ok(res);
            }
            else
            {
                return NotFound($"Style with ID {id} not found");
            }
        }
    }
}
