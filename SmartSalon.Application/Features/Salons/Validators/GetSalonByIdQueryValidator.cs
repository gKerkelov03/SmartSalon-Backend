
using FluentValidation;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.Features.Salons.Queries;

namespace SmartSalon.Application.Validators;

internal class GetSalonByIdQueryValidator : AbstractValidator<GetSalonByIdQuery>
{
    public GetSalonByIdQueryValidator()
    {
        RuleFor(query => query.SalonId).MustBeValidGuid();
    }
}