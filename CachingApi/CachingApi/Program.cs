using CachingApi;
using CachingApi.Cache;
using CachingApi.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDataRepository, DataRepository>();
builder.Services.AddScoped<IDataService, DataService>();

builder.Services.AddSingleton(sp => ConnectionMultiplexer.Connect("redis"));
builder.Services.AddSingleton<RedisGenericCache>();
builder.Services.Decorate<IDataRepository, RedisDataRepository>();

builder.Services.AddSingleton<IMemoryCache, MemoryCache>();
builder.Services.AddSingleton<InMemoryGenericCache>();
builder.Services.Decorate<IDataRepository, InMemoryDataRepository>();

builder.Services.AddHostedService<RedisSubscriberNotificationService>();
builder.Services.AddSingleton<IRedisSubscriber, RedisSubscriber>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

app.MapGet("/search", async ([FromQuery] string name, IDataService service) => await service.SearchEntities(name));
app.MapGet("/add", async ([FromQuery] string name, IDataService service) => await service.Add(name));


app.Run();
