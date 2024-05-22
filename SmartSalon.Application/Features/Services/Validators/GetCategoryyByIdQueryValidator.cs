
using FluentValidation;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.Features.Salons.Queries;

namespace SmartSalon.Application.Features.Services.Validators;

internal class GetCategoryByIdQueryValidator : AbstractValidator<GetCurrencyByIdQuery>
{
    public GetCategoryByIdQueryValidator()
    {
        RuleFor(query => query.CurrencyId).MustBeValidGuid();
    }
}