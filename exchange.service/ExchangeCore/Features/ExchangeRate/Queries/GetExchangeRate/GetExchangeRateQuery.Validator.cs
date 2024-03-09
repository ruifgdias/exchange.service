using FluentValidation;
using System;

namespace ExchangeCore.Features.ExchangeRate.Queries.GetExchangeRate
{
    public class GetExchangeRateQueryGetExchangeRateQuery
        : AbstractValidator<GetExchangeRateQuery>
    {
        public GetExchangeRateQueryGetExchangeRateQuery()
        {
            ApplyCurrencyRulesToProperty(query => query.FromCurrencyCode);
            ApplyCurrencyRulesToProperty(query => query.ToCurrencyCode);
        }

        public void ApplyCurrencyRulesToProperty<T>(System.Linq.Expressions.Expression<Func<GetExchangeRateQuery, T>> propertySelector)
        {
            RuleFor(propertySelector)
                .NotEmpty().WithMessage("The Currency Code can't be empty");
        }
    }
}
