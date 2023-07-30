using ServiceB.API.Dtos;
using ServiceB.API.Models;

namespace ServiceB.API.Services.Interfaces
{
    public interface IProductService
    {
        Task<Product> AddProductAsync(AddProductDTO @addProductDTO);
        Task<Product> DeleteProductByIdAsync(Guid ProductId);
        Task<List<Product>> GetAllProductAsync();
        Task<Product> GetProductById(Guid @ProductId);
    }
}
