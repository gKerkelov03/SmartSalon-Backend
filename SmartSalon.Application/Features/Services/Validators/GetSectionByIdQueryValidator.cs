
using FluentValidation;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.Features.Services.Queries;

namespace SmartSalon.Application.Features.Services.Validators;

internal class GetSectionByIdQueryValidator : AbstractValidator<GetSectionByIdQuery>
{
    public GetSectionByIdQueryValidator()
    {
        RuleFor(query => query.SectionId).MustBeValidGuid();
    }
}