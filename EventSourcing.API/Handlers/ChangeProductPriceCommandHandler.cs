using EventSourcing.API.Command;
using EventSourcing.API.EventStore;
using MediatR;

namespace EventSourcing.API.Handlers
{
    public class ChangeProductPriceCommandHandler : IRequestHandler<ChangeProductPriceCommand>
    {
        private readonly ProductStream _productStream;

        public ChangeProductPriceCommandHandler(ProductStream productStream)
        {
            _productStream = productStream;
        }
        public async Task<Unit> Handle(ChangeProductPriceCommand request, CancellationToken cancellationToken)
        {
            _productStream.PriceChanged(request.changeProductPriceDto);
            await _productStream.SaveAsync();
            return Unit.Value;
        }
    }
}
