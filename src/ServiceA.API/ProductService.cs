using ServiceA.API.Models;

namespace ServiceA.API
{
    public class ProductService
    {

        private readonly HttpClient _httpClient;
        private readonly ILogger<ProductService> _logger;

        public ProductService(HttpClient httpClient, ILogger<ProductService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<Products> GetProductById(Guid productId)
        {
            var product = await _httpClient.GetFromJsonAsync<Products>($"GetProductById/{productId}");
            _logger.LogInformation($"Products : {product.ProductId}-{product.ProductName}");
            return product;
        }
    }
}
