using ExchangeRate.ExternalClient.Models;
using Newtonsoft.Json;

namespace ExchangeRate.ExternalClient.Implementations
{
    public class AlphaVantageClient : IExchangeRateClient
    {
        private readonly HttpClient httpClient;
        private readonly string apiKey;

        public AlphaVantageClient(HttpClient httpClient,
                                  string apiKey)
        {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this.apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
            this.httpClient.BaseAddress = new Uri("https://www.alphavantage.co/"); // this url can be a setting on appsettings
        }

        public async Task<CurrencyExchangeCurrencyRateResponse> GetCurrencyExchangeRateAsync(string fromCurrency,
                                                                                             string toCurrency,
                                                                                             CancellationToken cancellationToken)
        {
            var response = await httpClient.GetAsync($"query?function=CURRENCY_EXCHANGE_RATE&from_currency={fromCurrency}&to_currency={toCurrency}&apikey={apiKey}", cancellationToken);
            response.EnsureSuccessStatusCode();

            var responseContentString = await response.Content.ReadAsStringAsync(cancellationToken);
            var responseData = JsonConvert.DeserializeObject<CurrencyExchangeResponse>(responseContentString);

            return responseData?.ExchangeCurrencyRate;
        }
    }
}
