using AutoMapper;
using EventSourcing.API.Context;
using EventSourcing.API.Dtos;
using EventSourcing.API.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventSourcing.API.Handlers
{
    public class GetProductAllListByUserIdHandler : IRequestHandler<GetProductAllListByUserId, List<ProductDto>>
    {
        private readonly ApplicationDbContext _applicationDbContext;


        public GetProductAllListByUserIdHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<List<ProductDto>> Handle(GetProductAllListByUserId request, CancellationToken cancellationToken)
        {
            
            var products = await _applicationDbContext.products
                .Where(x => x.UserId == request.UserId).ToListAsync();

            return products.Select(x=> new ProductDto { 
                Id = x.Id,Stock = x.Stock,
                Name = x.Name, Price = x.Price,UserId = x.UserId}).ToList();
        }
    }
}
