using DTOs;
using Microsoft.AspNetCore.Mvc;
using Services;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordController : ControllerBase
    {
        IPasswordService _p;
        ILogger<PasswordController> _logger;
        public PasswordController(IPasswordService ip, ILogger<PasswordController> logger)
        {
            _p = ip;
            _logger = logger;
        }


        [HttpPost]
        
        public int Post([FromBody] string p)
        {
            return _p.getStrengthByPassword(p);
        }


     
     
    }
}
