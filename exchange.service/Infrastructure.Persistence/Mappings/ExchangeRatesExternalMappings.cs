using ExchangeCurrencyRateEventPublisher;
using ExchangeRate.ExternalClient.Models;

namespace Infrastructure.Persistence.Mappings
{
    public static class ExchangeRatesExternalMappings
    {
        public static Models.ExchangeRate ToExchangeRate(this CurrencyExchangeCurrencyRateResponse currencyExchangeRateResponse)
        {
            if (currencyExchangeRateResponse == null)
            {
                return null;
            }

            return new Models.ExchangeRate
            {
                FromCurrencyCode = currencyExchangeRateResponse.FromCurrencyCode,
                ToCurrencyCode = currencyExchangeRateResponse.ToCurrencyCode,
                AskPrice = currencyExchangeRateResponse.AskPrice,
                BidPrice = currencyExchangeRateResponse.BidPrice,
            };
        }

        public static ExchangeCurrencyRateChangeEvent ToExchangeRateEvent(this Models.ExchangeRate exchangeRate)
        {
            if (exchangeRate == null)
            {
                return null;
            }

            return new ExchangeCurrencyRateChangeEvent
            {
                FromCurrencyCode = exchangeRate.FromCurrencyCode,
                ToCurrencyCode = exchangeRate.ToCurrencyCode,
                AskPrice = exchangeRate.AskPrice,
                BidPrice = exchangeRate.BidPrice,
                EventDateTimeUtc = DateTime.UtcNow,
            };
        }
    }
}
