using Newtonsoft.Json;

namespace ExchangeRate.ExternalClient.Models
{
    public class CurrencyExchangeResponse
    {
        [JsonProperty("Realtime Currency Exchange Rate")]
        public CurrencyExchangeCurrencyRateResponse ExchangeCurrencyRate { get; set; }
    }

    public class CurrencyExchangeCurrencyRateResponse
    {
        [JsonProperty("1. From_Currency Code")]
        public string FromCurrencyCode { get; set; }

        [JsonProperty("2. From_Currency Name")]
        public string FromCurrencyName { get; set; }

        [JsonProperty("3. To_Currency Code")]
        public string ToCurrencyCode { get; set; }

        [JsonProperty("4. To_Currency Name")]
        public string ToCurrencyName { get; set; }

        [JsonProperty("5. Exchange Rate")]
        public double ExchangeRate { get; set; }

        [JsonProperty("6. Last Refreshed")]
        public string LastRefreshed { get; set; }

        [JsonProperty("7. Time Zone")]
        public string TimeZone { get; set; }

        [JsonProperty("8. Bid Price")]
        public double BidPrice { get; set; }

        [JsonProperty("9. Ask Price")]
        public double AskPrice { get; set; }
    }
}
