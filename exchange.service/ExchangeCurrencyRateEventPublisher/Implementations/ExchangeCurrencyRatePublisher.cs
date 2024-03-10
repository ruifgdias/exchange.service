using Infrastructure.CrossCutting.Settings;
using MassTransit;
using Microsoft.Extensions.Options;

namespace ExchangeCurrencyRateEventPublisher.Implementations
{
    public class ExchangeCurrencyRatePublisher : IExchangeCurrencyRatePublisher
    {
        private readonly IBus bus;
        private readonly IOptions<RabbitMqSettings> rabbitMqSettings;

        public ExchangeCurrencyRatePublisher(IBus bus, IOptions<RabbitMqSettings> rabbitMqSettings)
        {
            this.bus = bus;
            this.rabbitMqSettings = rabbitMqSettings;
        }

        public async Task Send(ExchangeCurrencyRateChangeEvent trackingEvent, CancellationToken cancellationToken)
        {         
            var endpoint = await this.bus.GetSendEndpoint(new Uri($"queue:{this.rabbitMqSettings.Value.QueueName}"));
            await endpoint.Send(trackingEvent, cancellationToken);
        }
    }
}
