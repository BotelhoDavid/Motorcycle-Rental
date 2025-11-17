using LogAPI;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System.Collections;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IMongoClient>(_ =>
{
    IDictionary vars = Environment.GetEnvironmentVariables();

    string connectionString = vars["MONGO_CONNECTIONSTRING"]?.ToString() ?? string.Empty;

    return new MongoClient(connectionString);
});

builder.Services.AddHostedService<MotoNotificationConsumer>();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapGet("/", () => "RabbitMQ Consumer + MongoDB API running!");

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
