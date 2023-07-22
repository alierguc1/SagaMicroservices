using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.Business.Services.Interfaces;
using Order.Entity.Dtos;

namespace Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderServices _orderServices;
        public OrdersController(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderCreateDto orderCreateDto)
        {
            try
            {
                await _orderServices.CreateOrder(orderCreateDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
           
        }
    }
}
