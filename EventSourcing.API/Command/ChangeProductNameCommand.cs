using EventSourcing.API.Dtos;
using MediatR;

namespace EventSourcing.API.Command
{
    public class ChangeProductNameCommand : IRequest
    {
        public ChangeProductNameDto ChangeProductNameDto { get; set; }
    }
}
