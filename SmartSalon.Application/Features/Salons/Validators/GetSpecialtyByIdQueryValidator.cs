
using FluentValidation;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.Features.Salons.Queries;

namespace SmartSalon.Application.Features.Salons.Validators;

internal class GetSpecialtyByIdQueryValidator : AbstractValidator<GetSpecialtyByIdQuery>
{
    public GetSpecialtyByIdQueryValidator()
    {
        RuleFor(query => query.SpecialtyId).MustBeValidGuid();
    }
}