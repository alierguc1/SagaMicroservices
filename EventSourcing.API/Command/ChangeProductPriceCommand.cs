using EventSourcing.API.Dtos;
using MediatR;

namespace EventSourcing.API.Command
{
    public class ChangeProductPriceCommand : IRequest
    {
        public ChangeProductPriceDto changeProductPriceDto { get; set; }
    }
}
