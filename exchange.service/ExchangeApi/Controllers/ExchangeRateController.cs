using ExchangeCore.Features.ExchangeRate.Queries.GetExchangeRate;
using Infrastructure.Persistence.Models;
using Infrastructure.Persistence.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeApi.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ExchangeRateController : ControllerBase
    {
        private readonly ILogger<ExchangeRateController> _logger;
        private readonly IMediator _mediator;
        private readonly IExchangeRatesService service;

        public ExchangeRateController(ILogger<ExchangeRateController> logger,
                                      IMediator mediator, IExchangeRatesService service)
        {
            _logger = logger;
            _mediator = mediator;
            this.service = service;
        }

        [HttpGet(Name = "GetExchangeRate")]
        public Task<GetExchangeRateQueryResponse> Get([FromQuery] GetExchangeRateQuery query)
        {
            this._logger.LogInformation("Exchange Rate Request From:{0}, To:{1}", query.FromCurrencyCode, query.ToCurrencyCode);

            return this._mediator.Send(query);
        }

        [HttpPost]
        public async Task Post(Infrastructure.Persistence.Models.ExchangeRate e, CancellationToken cancellationToken)
        {
            await this.service.CreateAsync(e, cancellationToken);
        }
    }
}
