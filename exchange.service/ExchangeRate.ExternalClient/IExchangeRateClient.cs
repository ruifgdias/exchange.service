using ExchangeRate.ExternalClient.Models;

namespace ExchangeRate.ExternalClient
{
    public interface IExchangeRateClient
    {
        Task<CurrencyExchangeCurrencyRateResponse> GetCurrencyExchangeRateAsync(
            string fromCurrency,
            string toCurrency,
            CancellationToken cancellationToken = default);
    }
}
