using MassTransit;
using Microsoft.Extensions.Logging;
using Order.DataAccess.Context;
using Order.Entity.Enums;
using Shared.Events.payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.DataAccess.Consumers
{
    public class PaymentCompletedEventConsumer : IConsumer<PaymentCompletedEvent>
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<PaymentCompletedEvent> _logger;

        public PaymentCompletedEventConsumer(AppDbContext appDbContext, ILogger<PaymentCompletedEvent> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<PaymentCompletedEvent> context)
        {
            var order = await _appDbContext.Orders.FindAsync(context.Message.OrderId);
            if (order != null) {
                order.Status = OrderStatus.Completed;
                await _appDbContext.SaveChangesAsync();
                _logger.LogInformation($"Order Id : {context.Message.OrderId} Status Completed.");
            }
            else
            {
                _logger.LogError($"Not Found Order Id : {context.Message.OrderId}");
            }
        }
    }
}
