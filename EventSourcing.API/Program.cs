using EventSourcing.API.BackgroundServices;
using EventSourcing.API.Context;
using EventSourcing.API.EventStore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddEventStoreExtension(builder.Configuration);
builder.Services.AddSingleton<ProductStream>();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddHostedService<ProductReadModelEventStore>();

// Uygulama aya�a kalkt���nda backgroundService �al��s�n demektir.
//builder.Services.AddHostedService<ProductReadModelEventStore>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
