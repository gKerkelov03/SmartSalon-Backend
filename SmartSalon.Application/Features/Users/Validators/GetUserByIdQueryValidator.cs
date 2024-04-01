using FluentValidation;
using SmartSalon.Application.Features.Users.Queries;

namespace SmartSalon.Application.Validators;

internal class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdQueryValidator()
    {
    }
}