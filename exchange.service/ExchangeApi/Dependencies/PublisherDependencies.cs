using ExchangeCurrencyRateEventPublisher.Implementations;
using ExchangeCurrencyRateEventPublisher;
using MassTransit;
using Infrastructure.CrossCutting.Settings;

namespace ExchangeApi.Dependencies
{
    public static class PublisherDependencies
    {
        public static IServiceCollection AddRabbitMqEventPublisher(this IServiceCollection services, WebApplicationBuilder builder)
        {
            // Add RabbitMq Settings
            builder.Services.Configure<RabbitMqSettings>(
                builder.Configuration.GetSection(RabbitMqSettings.SettingName));

            // Add EventPublisher
            services.AddSingleton<IExchangeCurrencyRatePublisher, ExchangeCurrencyRatePublisher>();

            // Add RabbitMq
            services.AddMassTransit(x =>
            {
                var rabbitMqSettings = builder.Configuration.GetSection(RabbitMqSettings.SettingName).Get<RabbitMqSettings>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host($"rabbitmq://{rabbitMqSettings!.Uri}/", c =>
                    {
                        c.Username(rabbitMqSettings.Username);
                        c.Password(rabbitMqSettings.Password);
                    });
                });
            });

            return services;
        }
    }
}
