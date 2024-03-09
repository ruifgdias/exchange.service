using MediatR;

namespace ExchangeCore.Features.ExchangeRate.Queries.GetExchangeRate
{
    public record GetExchangeRateQuery(string FromCurrencyCode, string ToCurrencyCode)
        : IRequest<GetExchangeRateQueryResponse>;
}
