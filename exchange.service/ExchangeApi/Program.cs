using ExchangeCore;
using ExchangeRate.ExternalClient.Settings;
using Infrastructure.CrossCutting.Settings;
using Infrastructure.Persistence.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ExchangeRate.ExternalClient;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();
builder.Services.AddHttpClient();

// Add Mongo Settings
builder.Services.Configure<MongoSettings>(
    builder.Configuration.GetSection(MongoSettings.PropertyName));
builder.Services.AddSingleton<IExchangeRatesService>(x => 
    new ExchangeRatesService(x.GetRequiredService<IOptions<MongoSettings>>(), x.GetRequiredService<IExchangeRateClient>()));

// Add AlphaVillage Settings
builder.Services.Configure<AlphaVantageSettings>(
    builder.Configuration.GetSection(AlphaVantageSettings.PropertyName));
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
