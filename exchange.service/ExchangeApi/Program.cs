using ExchangeApi.Dependencies;
using ExchangeCore;
using ExchangeCurrencyRateEventPublisher;
using ExchangeCurrencyRateEventPublisher.Implementations;
using ExchangeRate.ExternalClient;
using ExchangeRate.ExternalClient.Settings;
using Infrastructure.CrossCutting.Settings;
using Infrastructure.Persistence.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();
builder.Services.AddHttpClient();

// Add RabbitMq Producer
builder.Services.AddRabbitMqEventPublisher(builder);

// Add Mongo Settings
builder.Services.Configure<MongoSettings>(
    builder.Configuration.GetSection(MongoSettings.SettingName));
builder.Services.AddSingleton<IExchangeRatesService>(x => 
    new ExchangeRatesService(x.GetRequiredService<IOptions<MongoSettings>>(),
                             x.GetRequiredService<IExchangeRateClient>(),
                             x.GetRequiredService<IExchangeCurrencyRatePublisher>()));

// Add AlphaVillage Settings
builder.Services.Configure<AlphaVantageSettings>(
    builder.Configuration.GetSection(AlphaVantageSettings.SettingName));
builder.Services.AddAlphaVantageClient();

builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
    config.ReportApiVersions = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
