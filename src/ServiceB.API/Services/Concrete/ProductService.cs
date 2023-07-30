using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ServiceB.API.ApplicationDBContext;
using ServiceB.API.Dtos;
using ServiceB.API.Models;
using ServiceB.API.Services.Interfaces;

namespace ServiceB.API.Services.Concrete
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        public ProductService(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<Product> AddProductAsync(AddProductDTO addProductDTO)
        {
            var addProduct = _mapper.Map<Product>(addProductDTO);
            await _appDbContext.AddAsync(addProduct);
            await _appDbContext.SaveChangesAsync();
            return addProduct;
        }

        public async Task<Product> DeleteProductByIdAsync(Guid ProductId)
        {
            var getByProduct = await _appDbContext.Products.FindAsync(ProductId);
            var result = _appDbContext.Remove(getByProduct);
            await _appDbContext.SaveChangesAsync();
            return getByProduct;
        }

        public async Task<List<Product>> GetAllProductAsync()
        {
            return await _appDbContext.Products.ToListAsync();
        }

        public async Task<Product> GetProductById(Guid ProductId)
        {
            return await _appDbContext.Products
                .Where(x => x.ProductId == ProductId).FirstOrDefaultAsync();
        }
    }
}
