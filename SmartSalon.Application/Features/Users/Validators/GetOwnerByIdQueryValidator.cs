using FluentValidation;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.Features.Users.Queries;

namespace SmartSalon.Application.Validators;

internal class GetOwnerByIdQueryValidator : AbstractValidator<GetOwnerByIdQuery>
{
    public GetOwnerByIdQueryValidator()
    {
        RuleFor(query => query.OwnerId).MustBeValidGuid();
    }
}