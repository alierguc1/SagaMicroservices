using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Payment.API.Dto;
using Payment.API.RedisBuilder.Interfaces;

namespace Payment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ICacheService _cacheService;
        public PaymentController(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }


        [HttpPost("CreateBuyerPayment")]
        public async Task<IActionResult> CreateBuyerPayment(CreatePaymentDto createPaymentDto)
        {
            return Ok(await _cacheService.SetValueAsync(createPaymentDto.BuyerId, createPaymentDto.balance));
        }
    }
}
