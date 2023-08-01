using MassTransit;
using Microsoft.Extensions.Logging;
using Shared.Events.payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Stock.Infrastructure.Consumers
{
    public class PaymentFailedEventConsumer : IConsumer<PaymentFailEvent>
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<OrderCreatedEventConsumer> _logger;

        public PaymentFailedEventConsumer(AppDbContext appDbContext, ILogger<OrderCreatedEventConsumer> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<PaymentFailEvent> context)
        {

            foreach (var item in context.Message.OrderItems)
            {
                var stock = await _appDbContext.OrderStocks.FirstOrDefaultAsync(x=>x.ProductId == item.ProductId);
                if(stock != null)
                {
                    stock.Count += item.Count;
                    await _appDbContext.SaveChangesAsync();
                }
            }
            _logger.LogInformation($"Stoklar Eski Haline Getirildi. Sipariş Id : {context.Message.OrderId}");

        }
    }
}
