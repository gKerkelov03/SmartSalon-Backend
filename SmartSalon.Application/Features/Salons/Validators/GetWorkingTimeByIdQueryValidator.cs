
using FluentValidation;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.Features.Salons.Queries;

namespace SmartSalon.Application.Validators;

internal class GetWorkingTimeByIdQueryValidator : AbstractValidator<GetWorkingTimeByIdQuery>
{
    public GetWorkingTimeByIdQueryValidator()
    {
        RuleFor(query => query.WorkingTimeId).MustBeValidGuid();
    }
}