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
    }
}
