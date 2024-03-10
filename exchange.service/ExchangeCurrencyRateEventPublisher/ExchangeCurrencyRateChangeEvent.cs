namespace ExchangeCurrencyRateEventPublisher
{
    public class ExchangeCurrencyRateChangeEvent
    {
        public string FromCurrencyCode { get; set; }

        public string ToCurrencyCode { get; set; }

        public double BidPrice { get; set; }

        public double AskPrice { get; set; }

        public DateTime EventDateTimeUtc { get; set; }
    }
}
