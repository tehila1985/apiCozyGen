using DTOs;
using Microsoft.AspNetCore.Mvc;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }



        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<DtoOrder_Id_UserId_Date_Sum_OrderItems>>> GetByUser(int userId)
        {
            var order = await _orderService.GetOrdersUser(userId);
            if (order != null)
            {
                return Ok(order);
            }
            return NoContent();
        }

        // GET api/<OrderController>/5
        // GET api/<users>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DtoOrder_Id_UserId_Date_Sum_OrderItems>> Get(int id)
        {
            DtoOrder_Id_UserId_Date_Sum_OrderItems order = await _orderService.GetOrderById(id);
            if (order != null)
            {
                return Ok(order);
            }
            return NoContent();
        }

        // POST api/<users>
        [HttpPost]
        public async Task<ActionResult<DtoOrder_Id_UserId_Date_Sum_OrderItems>> Post([FromBody] DtoOrder_Id_UserId_Date_Sum_OrderItems order)
        {

            DtoOrder_Id_UserId_Date_Sum_OrderItems res = await _orderService.AddNewOrder(order);
            if (res != null)
            {
                return CreatedAtAction(nameof(Get), new { id = res.OrderId }, res);
            }
            else
                return BadRequest();
        }

    }
}