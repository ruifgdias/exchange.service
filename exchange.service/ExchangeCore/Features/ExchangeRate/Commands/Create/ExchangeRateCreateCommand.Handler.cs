using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ExchangeCore.Features.ExchangeRate.Commands.Create
{
    public class ExchangeRateCreateCommandHandler : IRequestHandler<ExchangeRateCreateCommand>
    {
        public Task Handle(ExchangeRateCreateCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
