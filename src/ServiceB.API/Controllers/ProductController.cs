using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceB.API.Dtos;
using ServiceB.API.Services.Interfaces;

namespace ServiceB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            return  Ok(await _productService.GetAllProductAsync());
        }


        [HttpGet("GetProductById/{productId}")]
        public async Task<IActionResult> GetProductById(Guid productId)
        {
            return Ok(await _productService.GetProductById(productId));
        }

        [HttpDelete("DeleteProduct/{productId}")]
        public async Task<IActionResult> DeleteProduct(Guid productId)
        {

            return Ok(await _productService.DeleteProductByIdAsync(productId));
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct(AddProductDTO addProductDTO)
        {
      
            return Ok(await _productService.AddProductAsync(addProductDTO));
        }
    }
}
