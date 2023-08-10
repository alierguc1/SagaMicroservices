using EventSourcing.API.Context;
using EventSourcing.API.EventStore;
using EventSourcing.API.Models;
using EventSourcing.Shared.Events;
using EventStore.ClientAPI;
using System;
using System.Text;
using System.Text.Json;

namespace EventSourcing.API.BackgroundServices
{
    public class ProductReadModelEventStore : BackgroundService
    {
        private readonly IEventStoreConnection _eventStoreConnection;
        private readonly ILogger<ProductReadModelEventStore> _logger;

        private readonly IServiceProvider _serviceProvider;

        public ProductReadModelEventStore(IEventStoreConnection eventStoreConnection, ILogger<ProductReadModelEventStore> logger, IServiceProvider serviceProvider)
        {
            _eventStoreConnection = eventStoreConnection;
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _eventStoreConnection.ConnectToPersistentSubscriptionAsync(ProductStream.StreamName, ProductStream.GroupName, EventAppeared, autoAck: false);
            // true:  EventAppeared exception firlamadı ise event gönderildi sayar.
            //false :
        }

        private async Task EventAppeared(EventStorePersistentSubscriptionBase arg1, ResolvedEvent arg2)
        {
            var type = Type.GetType($"{Encoding.UTF8.GetString(arg2.Event.Metadata)}, EventSourcing.Shared");
            _logger.LogInformation($"The Message processing... : {type.ToString()}");
            var eventData = Encoding.UTF8.GetString(arg2.Event.Data);

            var @event = JsonSerializer.Deserialize(eventData, type);

            using var scope = _serviceProvider.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            Product product = null;

            switch (@event)
            {
                case ProductCreatedEvent productCreatedEvent:

                    product = new Product()
                    {
                        Name = productCreatedEvent.Name,
                        Id = productCreatedEvent.Id,
                        Price = productCreatedEvent.Price,
                        Stock = productCreatedEvent.Stock,
                        UserId = productCreatedEvent.UserId
                    };
                    context.products.Add(product);
                    await context.SaveChangesAsync();
                    break;

                case ProductNameChangeEvent productNameChangedEvent:

                    product = context.products.Find(productNameChangedEvent.Id);
                    if (product != null)
                    {
                        product.Name = productNameChangedEvent.ChangedName;
                    }
                    await context.SaveChangesAsync();
                    break;

                case ProductPriceChangeEvent productPriceChangedEvent:
                    product = context.products.Find(productPriceChangedEvent.Id);
                    if (product != null)
                    {
                        product.Price = productPriceChangedEvent.ChangedPrice;
                    }
                    await context.SaveChangesAsync();
                    break;

                case ProductDeletedEvent productDeletedEvent:
                    product = context.products.Find(productDeletedEvent.Id);
                    if (product != null)
                    {
                        context.products.Remove(product);
                    }
                    await context.SaveChangesAsync();
                    break;
            }

            arg1.Acknowledge(arg2.Event.EventId);
        }
    }
}
