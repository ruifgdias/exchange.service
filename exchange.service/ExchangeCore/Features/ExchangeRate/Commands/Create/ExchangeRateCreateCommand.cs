using MediatR;

namespace ExchangeCore.Features.ExchangeRate.Commands.Create
{
    public record ExchangeRateCreateCommand(string FromCurrencyCode,
                                            string ToCurrencyCode,
                                            double BidPrice,
                                            double AskPrice)
        : IRequest;
}
