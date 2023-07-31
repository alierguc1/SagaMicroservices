using Polly.Extensions.Http;
using Polly;
using ServiceA.API;
using System.Diagnostics;
using Polly.CircuitBreaker;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//var circuitBreakerPolicy = GetCircuitBreakerPolicy();
var circuitBreakerPolicy = GetAdvancedCircuitBreakerPolicy();
builder.Services.AddHttpClient<ProductService>(x =>
{
    x.BaseAddress = new Uri("http://localhost:5001/api/product/");
}).AddPolicyHandler(circuitBreakerPolicy);


IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
{
    return HttpPolicyExtensions
        .HandleTransientHttpError()
        .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30), onBreak: (arg1, arg2) =>
        {
            Debug.WriteLine($"Circuit Breaker Status => On Break :");
        }, onReset: () =>
        {
            Debug.WriteLine($"Circuit Breaker Status => On Reset :");
        }, onHalfOpen: () =>
        {
            Debug.WriteLine($"Circuit Breaker Status => On Half Open :");
        });
}
IAsyncPolicy<HttpResponseMessage> GetAdvancedCircuitBreakerPolicy()
{
    return HttpPolicyExtensions
        .HandleTransientHttpError()
        // 30 saniyede gelen istek sayýsýnýn yüzde 10 u baþarýsýzsa devreye gir. 30 saniye sonra sýfýrla.
        .AdvancedCircuitBreakerAsync(0.1,TimeSpan.FromSeconds(30),5,TimeSpan.FromSeconds(30), onBreak: (arg1, arg2) =>
        {
            Debug.WriteLine($"Circuit Breaker Status => On Break :");
        }, onReset: () =>
        {
            Debug.WriteLine($"Circuit Breaker Status => On Reset :");
        }, onHalfOpen: () =>
        {
            Debug.WriteLine($"Circuit Breaker Status => On Half Open :");
        });
}


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
