using AutoMapper;
using ServiceB.API.Dtos;
using ServiceB.API.Models;

namespace ServiceB.API.Mapper
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            this.CreateMap<Product, AddProductDTO>();
            this.CreateMap<AddProductDTO, Product>();
        }
    }
}
