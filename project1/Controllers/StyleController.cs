using Dto;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StyleController : ControllerBase
    {
        private readonly ILogger<StyleController> _logger;
        private readonly IStyleService _s;
        private readonly IUserServices _userService; // הוספת שירות המשתמשים

        public StyleController(IStyleService i, ILogger<StyleController> logger, IUserServices userService)
        {
            _s = i;
            _logger = logger;
            _userService = userService; // הזרקה בבנאי
        }

        // GET: api/<StyleController>
        [HttpGet]
        public async Task<IEnumerable<DtoSyle_id_name>> Get()
        {
            return await _s.GetStyles();
        }

        [HttpPost]
        public async Task<ActionResult<DtoSyle_id_name>> Post(
            [FromBody] DtoStyleAll StyleDto,
            [FromHeader] int userId,
            [FromHeader] string password)
        {
            bool isAdmin = await _userService.IsAdminById(userId, password);
            if (!isAdmin)
            {
                return Forbid("גישה נדחתה: דרושות הרשאות מנהל להוספת סגנון");
            }

            DtoSyle_id_name res = await _s.AddNewStyle(StyleDto);
            if (res != null)
            {
                return Ok(res);
            }
            return BadRequest("נכשל בהוספת הסגנון");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DtoSyle_id_name>> Delete(
            int id,
            [FromHeader] int userId,
            [FromHeader] string password)
        {
            bool isAdmin = await _userService.IsAdminById(userId, password);
            if (!isAdmin)
            {
                return Forbid("גישה נדחתה: דרושות הרשאות מנהל למחיקת סגנון");
            }

            DtoSyle_id_name res = await _s.Delete(id);

            if (res != null)
            {
                return Ok(res);
            }
            return NotFound($"Style with ID {id} not found");
        }
    }
}