namespace ExchangeCurrencyRateEventPublisher
{
    public interface IExchangeCurrencyRatePublisher
    {
        Task Send(ExchangeCurrencyRateChangeEvent trackingEvent, CancellationToken cancellationToken);
    }
}
