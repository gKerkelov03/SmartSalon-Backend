using FluentValidation;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.Features.Users.Queries;

namespace SmartSalon.Application.Features.Users.Validators;

internal class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdQueryValidator()
    {
        RuleFor(query => query.UserId).MustBeValidGuid();
    }
}