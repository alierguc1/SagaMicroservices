using MassTransit;
using Microsoft.Extensions.Logging;
using Order.DataAccess.Context;
using Order.Entity.Enums;
using Shared.Events.payment;
using Shared.Events.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.DataAccess.Consumers
{
    public class StockNotReservedEventConsumer : IConsumer<StockNotReservedEvent>
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<PaymentFailEvent> _logger;

        public StockNotReservedEventConsumer(AppDbContext appDbContext, ILogger<PaymentFailEvent> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<StockNotReservedEvent> context)
        {
            var order = await _appDbContext.Orders.FindAsync(context.Message.OrderId);
            if (order != null)
            {
                order.Status = OrderStatus.Fail;
                await _appDbContext.SaveChangesAsync();
                _logger.LogInformation($"Order Id : {context.Message.OrderId} Status Failed.");
            }
            else
            {
                _logger.LogError($"Not Found Order Id : {context.Message.OrderId}");
            }
        }
    }
}
