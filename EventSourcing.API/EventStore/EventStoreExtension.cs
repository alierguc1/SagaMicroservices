using EventStore.ClientAPI;

namespace EventSourcing.API.EventStore
{
    public static class EventStoreExtension
    {
        public static IServiceCollection AddEventStoreExtension(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = EventStoreConnection.Create(connectionString: configuration.GetConnectionString("EventStore"));
            connection.ConnectAsync().Wait();
            services.AddSingleton(connection);
            using var logFactory = LoggerFactory.Create
                (builder =>
                {
                    builder.SetMinimumLevel(LogLevel.Information);
                    builder.AddConsole();
                });
            var logger = logFactory.CreateLogger("Program");
            connection.Connected += (sender, args) =>
            {
                logger.LogInformation("EventStore Connection Established");
            };

            connection.Disconnected += (sender, args) =>
            {
                logger.LogInformation($"{args.ToString()} EventStore Connection Error");
            };

            return services;
        }
    }
}
