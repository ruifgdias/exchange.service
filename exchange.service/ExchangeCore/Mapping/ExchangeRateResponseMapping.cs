using ExchangeCore.Features.ExchangeRate.Queries.GetExchangeRate;
using Models = Infrastructure.Persistence.Models;

namespace ExchangeCore.Mapping
{
    public static class ExchangeRateResponseMapping
    {
        public static GetExchangeRateQueryResponse ToResponse(this Models.ExchangeRate? source)
        {
            if (source is null)
            {
                return null;
            }

            return new GetExchangeRateQueryResponse(source.FromCurrencyCode,
                                                    source.ToCurrencyCode,
                                                    source.BidPrice,
                                                    source.AskPrice);
        }
    }
}
