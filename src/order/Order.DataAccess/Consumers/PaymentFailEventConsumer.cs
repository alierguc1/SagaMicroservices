using MassTransit;
using Microsoft.Extensions.Logging;
using Shared.Events.payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Order.DataAccess.Context;
using System.Threading.Tasks;
using Order.Entity.Enums;

namespace Order.DataAccess.Consumers
{
    public class PaymentFailEventConsumer : IConsumer<PaymentFailEvent>
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<PaymentFailEvent> _logger;

        public PaymentFailEventConsumer(AppDbContext appDbContext, ILogger<PaymentFailEvent> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<PaymentFailEvent> context)
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
