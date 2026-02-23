using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;
using System.Text.Json;
using Services;
using static project1.Controllers.Userscontroller;
using Dto;
using Repository.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace project1.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class Userscontroller : ControllerBase
    {
        IUserServices _s;
        private readonly ILogger<Userscontroller> _logger;
        public Userscontroller(IUserServices i, ILogger<Userscontroller> logger)
        {
            _logger = logger;
            _s = i;
        }

        //GET: api/<users>
        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            return await _s.GetUsers();
        }

        // GET api/<users>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DtoUser_Name_Gmail>> Get(int id)
        {
            DtoUser_Name_Gmail user = await _s.GetUserById(id);
            if (user!=null)
            {
                return Ok(user);
            }       
          return NoContent();
        }
        // POST api/<users>

        [HttpPost]
        public async Task<ActionResult<DtoUser_Id_Name>> Post([FromBody] DtoUser_Name_Password_Gmail user)
        {

            DtoUser_Id_Name res = await _s.AddNewUser(user);
            if (res!=null)
            {
                return CreatedAtAction(nameof(Get), new { id = res.UserId }, res);
            }
            else
                return BadRequest();
        }

        //POST
        [HttpPost("Login")]
        public async Task<ActionResult<DtoUser_Id_Name>> Login([FromBody] DtoUser_Gmail_Password user)
        {
            DtoUser_Id_Name res = await _s.Login(user);
            if(res!=null)
            {
                _logger.LogInformation($"login attempted with user name,{user.Email} and password {user.PasswordHash}");
                return Ok(res);
            }  
            return NotFound();
        }



        // PUT api/<users>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<DtoUser_Id_Name>> Put(int id, [FromBody] DtoUser_Name_Password_Gmail value)
        {
            DtoUser_Id_Name res = await _s.update(id, value);
            if (res!= null)
            {
                return CreatedAtAction(nameof(Get), new { id = res.UserId }, res);
            }
            else
                return BadRequest();  
        }
    }
}
