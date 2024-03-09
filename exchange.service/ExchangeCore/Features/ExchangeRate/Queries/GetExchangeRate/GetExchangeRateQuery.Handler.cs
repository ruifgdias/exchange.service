using ExchangeCore.Mapping;
using Infrastructure.Persistence.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ExchangeCore.Features.ExchangeRate.Queries.GetExchangeRate
{
    public class GetExchangeRateQueryHandler : IRequestHandler<GetExchangeRateQuery, GetExchangeRateQueryResponse>
    {
        private readonly IExchangeRatesService exchangeRatesService;

        public GetExchangeRateQueryHandler(IExchangeRatesService exchangeRatesService)
        {
            this.exchangeRatesService = exchangeRatesService;
        }

        public async Task<GetExchangeRateQueryResponse> Handle(GetExchangeRateQuery request, CancellationToken cancellationToken)
        {
            var exchangeRate = await this.exchangeRatesService.GetAsync(request.FromCurrencyCode,
                                                                        request.ToCurrencyCode,
                                                                        cancellationToken);
            return exchangeRate.ToResponse();
        }
    }
}
