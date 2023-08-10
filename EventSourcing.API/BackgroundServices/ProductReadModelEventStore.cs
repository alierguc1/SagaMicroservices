using EventSourcing.API.Context;
using EventSourcing.API.EventStore;
using EventSourcing.API.Models;
using EventSourcing.Shared.Events;
using EventStore.ClientAPI;
using System.Text;
using System.Text.Json;

namespace EventSourcing.API.BackgroundServices
{
    public class ProductReadModelEventStore : BackgroundService
    {
        // Tüm bu işlemler, event store'a kaydedilen verilerden sonra, arkaplan'da eventstore'u dinleyip
        // veritabanına eventstoredan gelen verileri kaydetmek için kullanılır. Bu işlem için bir backgroundService
        // kullanılır. Event oluşturulduğu an dinler, subscribe olur ve veritabanına yazar.

        private readonly IEventStoreConnection _connection;
        private readonly ILogger<ProductReadModelEventStore> _logger;
        private readonly IServiceProvider _serviceProvider;
        
        public ProductReadModelEventStore(IEventStoreConnection connection, ILogger<ProductReadModelEventStore> logger, IServiceProvider serviceProvider)
        {
            _connection = connection;
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
            // autoAct : true olursa EventAppear metodu exception fırlatırsa event gönderildi sayılır.
            // autoAct : false olursa EventAppear metodu exception fırlatırsa event gönderme olayı ;
            // arg1.Acknowledge(arg2.Event.EventId); metoduna bağlı olarak manuel olur.
            await _connection.ConnectToPersistentSubscriptionAsync(
                 ProductStream.StreamName,
                 ProductStream.GroupName,
                 EventAppear,
                 autoAck:false);



            throw new NotImplementedException();
        }

        private async Task EventAppear(EventStorePersistentSubscriptionBase arg1, ResolvedEvent arg2)
        {
            // Mesajın İşlendiğine Dair Log
            _logger.LogInformation($"The message/messages processing ...");
           
            // Burada eventin metadata bilgisini alıyoruz. Buna göre event nasıl bir davranışta bulunuyor, o bilgiyi alıyoruz.
            var type = Type.GetType($"{Encoding.UTF8.GetString(arg2.Event.Metadata)}, EventSourcing.Shared");
            
            // Eventstore'da bulunan, göndermiş olduğumuz JSON verisi.
            var eventData = Encoding.UTF8.GetString(arg2.Event.Data);

            // Gelen eventData ile type verisini Deserialize etme işlemi.
            var @event = JsonSerializer.Deserialize(eventData, type);

            // DB context nesnesine erişebilmemiz için gereken scope mekanizmasıdır. 
            // IServiceProvider interface'i sistem üzerindeki nesnelere erişmek ve
            // scope oluşturmanın buradaki amacı, işlem düştüğünde memory'den silinsin diye.
            using var scope = _serviceProvider.CreateScope();

            // scope ile ApplicationDbContext nesnesi örneğini aşağıdaki şekilde alıyoruz.
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            Product product = null;

            // Gelen Event type'a göre kontrol ediyoruz ve ona göre veritabanı işlemleri yapıyoruz.
            switch (@event)
            {
                case ProductCreatedEvent productCreatedEvent :
                    product = new Product()
                    {
                        Name = productCreatedEvent.Name,
                        Id = productCreatedEvent.Id,
                        Stock = productCreatedEvent.Stock,
                        Price = productCreatedEvent.Price,
                        UserId = productCreatedEvent.UserId
                    };
                context.products.Add(product);
                break;

                case ProductNameChangeEvent productNameChangeEvent:
                    product = context.products.Find(productNameChangeEvent.Id);
                    if(product != null)
                    {
                        product.Name = productNameChangeEvent.ChangedName;
                    }
                break;

                case ProductPriceChangeEvent productPriceChangeEvent:
                    product = context.products.Find(productPriceChangeEvent.Id);
                    if (product != null)
                    {
                        product.Price = productPriceChangeEvent.ChangedPrice;
                    }
                break;

                case ProductDeletedEvent productDeletedEvent:
                    product = context.products.Find(productDeletedEvent.Id);
                    if (product != null)
                    {
                        context.products.Remove(product);
                    }
                break;
            }

            // veritabanı işlemlerini kaydediyoruz.
            await context.SaveChangesAsync();

            arg1.Acknowledge(arg2.Event.EventId);
        }
    }
}
