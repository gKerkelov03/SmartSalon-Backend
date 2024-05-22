using FluentValidation;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.Features.Users.Queries;

namespace SmartSalon.Application.Features.Users.Validators;

internal class GetWorkerByIdQueryValidator : AbstractValidator<GetWorkerByIdQuery>
{
    public GetWorkerByIdQueryValidator()
    {
        RuleFor(query => query.WorkerId).MustBeValidGuid();
    }
}