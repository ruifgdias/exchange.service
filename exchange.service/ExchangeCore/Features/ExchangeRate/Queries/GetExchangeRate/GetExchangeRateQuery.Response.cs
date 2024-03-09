namespace ExchangeCore.Features.ExchangeRate.Queries.GetExchangeRate
{
    public record GetExchangeRateQueryResponse(string FromCurrencyCode, string ToCurrencyCode, double BidPrice, double AskPrice);
}
