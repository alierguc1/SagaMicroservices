using EventSourcing.API.Command;
using EventSourcing.API.Dtos;
using EventSourcing.API.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventSourcing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
        {
            return Ok(await _mediator.Send(new CreateProductCommand() { CreateProductDto = createProductDto}));
        }

        [HttpGet("GetProductAllListByUserId/{userId}")]
        public async Task<IActionResult> GetProductAllListByUserId(int userId)
        {
            return Ok(await _mediator.Send(new GetProductAllListByUserId() { UserId = userId }));
        }

        [HttpPut("ChangeProductName")]
        public async Task<IActionResult> ChangeProductName(ChangeProductNameDto changeProductNameDto)
        {
            return Ok(await _mediator.Send(new ChangeProductNameCommand() { ChangeProductNameDto = changeProductNameDto }));
        }

        [HttpPut("ChangeProductPrice")]
        public async Task<IActionResult> ChangeProductPrice(ChangeProductPriceDto changeProductPriceDto)
        {
            return Ok(await _mediator.Send(new ChangeProductPriceCommand() { changeProductPriceDto = changeProductPriceDto }));
        }

        [HttpDelete("DeleteProduct/{Id}")]
        public async Task<IActionResult> DeleteProduct(Guid Id)
        {
            return Ok(await _mediator.Send(new DeleteProductCommand() { Id = Id }));
        }
    }
}
