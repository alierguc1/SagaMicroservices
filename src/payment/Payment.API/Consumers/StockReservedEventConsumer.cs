using MassTransit;
using Payment.API.RedisBuilder.Concrete;
using Payment.API.RedisBuilder.Interfaces;
using Shared.Events.payment;
using Shared.Events.Stock;

namespace Payment.API.Consumers
{
    public class StockReservedEventConsumer : IConsumer<StockReservedEvent>
    {
        private readonly ILogger<StockReservedEvent> _logger;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ICacheService _cacheService;

        public StockReservedEventConsumer(
            ILogger<StockReservedEvent> logger,
            IPublishEndpoint publishEndpoint,
            ICacheService cacheService)
        {
            _cacheService = cacheService;
            _logger = logger;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<StockReservedEvent> context)
        {
            var balance = _cacheService.GetValueAsync(context.Message.BuyerId.ToString());
            if (balance.Result != 0)
            {

                if (Convert.ToInt32(balance.Result) > context.Message.Payment.TotalPrice)
                {
                    _logger.LogInformation($"{context.Message.Payment.TotalPrice} Bakiyesi olan kişinin " +
                        $"id : ${context.Message.BuyerId} ödeme yapabilir.");

                    await _publishEndpoint.Publish(new PaymentCompletedEvent
                    {
                        BuyerId = context.Message.BuyerId,
                        OrderId = context.Message.OrderId
                    });
                }
                else
                {
                    _logger.LogInformation($"{context.Message.Payment.TotalPrice} Bakiyesi olan kişinin " +
                       $"id : ${context.Message.BuyerId} ödeme yapamaz.");

                    await _publishEndpoint.Publish(new PaymentFailEvent
                    {
                        BuyerId = context.Message.BuyerId,
                        OrderId = context.Message.OrderId,
                        Message = "Yetersiz Bakiye"
                    });


                }

            }
            else
            {
                _logger.LogInformation($"id : ${context.Message.BuyerId} Kişinin bakiye bilgisi bulunmamaktadır.");

                await _publishEndpoint.Publish(new PaymentFailEvent
                {
                    BuyerId = context.Message.BuyerId,
                    OrderId = context.Message.OrderId,
                    Message = "Yetersiz Bakiye"
                });

            }

        }
    }
}
