using MassTransit;
using Payment.API.Consumers;
using Payment.API.RedisBuilder.Concrete;
using Payment.API.RedisBuilder.Interfaces;
using Shared.Settings;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICacheService, CacheService>();
IConfiguration configuration = builder.Configuration;
var multiplexer = ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis"));
builder.Services.AddSingleton<IConnectionMultiplexer>(multiplexer);
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<StockReservedEventConsumer>();
    x.UsingRabbitMq((context, conf) =>
    {
        conf.Host(configuration.GetConnectionString("RabbitMqUri"));
        conf.ReceiveEndpoint(RabbitMQSettingsConst.STOCK_RESERVED_EVENT_QUEUE_NAME, e =>
        {
            e.ConfigureConsumer<StockReservedEventConsumer>(context);
        });
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
