using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ServiceA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetByProductId/{productId}")]
        public async Task<IActionResult> GetByProductId(Guid productId) { 
            return Ok(await _productService.GetProductById(productId));
        }
    }
}
