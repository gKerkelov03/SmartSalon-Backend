
using FluentValidation;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.Features.Salons.Queries;

namespace SmartSalon.Application.Validators;

internal class GetCurrencyByIdQueryValidator : AbstractValidator<GetCurrencyByIdQuery>
{
    public GetCurrencyByIdQueryValidator()
    {
        RuleFor(query => query.CurrencyId).MustBeValidGuid();
    }
}