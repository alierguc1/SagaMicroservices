using MassTransit;
using MassTransit.Transports;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared.Events.order;
using Shared.Events.Stock;
using Shared.Settings;
using Stock.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Infrastructure.Consumers
{
    public class OrderCreatedEventConsumer : IConsumer<OrderCreatedEvent>
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<OrderCreatedEventConsumer> _logger;
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IPublishEndpoint _publishEndpoint;

        public OrderCreatedEventConsumer(
            AppDbContext appDbContext,
            ILogger<OrderCreatedEventConsumer> logger,
            ISendEndpointProvider sendEndpointProvider,
            IPublishEndpoint publishEndpoint)
        {
            _appDbContext = appDbContext;
            _logger = logger;
            _sendEndpointProvider = sendEndpointProvider;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            var stockResult = new List<bool>();

            foreach (var item in context.Message.OrderItem)
            {
                try
                {
                    stockResult.Add(await _appDbContext.OrderStocks
                    .AnyAsync(y => y.ProductId == item.ProductId && y.Count > item.Count));
                }
                catch (Exception ex)
                {
                    _logger.LogInformation($"Ürün Id'li : {context.Message.BuyerId} kullanıcı tarafından rezerve edildi.");
                }
  
            }

            if (stockResult.All(x => x.Equals(true))){
                foreach (var item in context.Message.OrderItem)
                {
                    var stock = await _appDbContext.OrderStocks.FirstOrDefaultAsync(x => x.ProductId == item.ProductId);
                    if(stock != null)
                    {
                        stock.Count-=item.Count;
                    }
                    await _appDbContext.SaveChangesAsync();
                }
                _logger.LogInformation($"Ürün Id'li : {context.Message.BuyerId} kullanıcı tarafından rezerve edildi.");

                var sendEndpoint = await _sendEndpointProvider
                    .GetSendEndpoint(new Uri($"queue:{RabbitMQSettingsConst.STOCK_RESERVED_EVENT_QUEUE_NAME}"));

                StockReservedEvent stockReservedEvent = new StockReservedEvent()
                {
                    Payment = context.Message.Payment,
                    BuyerId = context.Message.BuyerId,
                    OrderId = context.Message.OrderId,
                    OrderItem = context.Message.OrderItem
                };

                await sendEndpoint.Send(stockReservedEvent);
            }
            else
            {
                await _publishEndpoint.Publish(new StockNotReservedEvent()
                {
                    OrderId= context.Message.OrderId,
                    Message = "Yeterli sayıda ürün stokta yok."
                });
            }
        }
    }
}
