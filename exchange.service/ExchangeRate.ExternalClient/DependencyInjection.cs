using ExchangeRate.ExternalClient.Implementations;
using ExchangeRate.ExternalClient.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ExchangeRate.ExternalClient
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAlphaVantageClient(this IServiceCollection services)
        {
            services.AddSingleton<IExchangeRateClient>(x =>
            {
                var settings = x.GetRequiredService<IOptions<AlphaVantageSettings>>();
                var httpClientFactory = x.GetRequiredService<IHttpClientFactory>();
                var httpClient = httpClientFactory.CreateClient();

                return new AlphaVantageClient(httpClient, settings.Value.ApiKey);
            });

            return services;
        }
    }
}
