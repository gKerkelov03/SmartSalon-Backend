using FluentValidation;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.Features.Users.Commands;
using static SmartSalon.Application.ApplicationConstants.Validation.User;

namespace SmartSalon.Application.Features.Users.Validators;

internal class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(command => command.Password).MustBeValidPassword();

        RuleFor(command => command.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(MaxEmailLength);
    }
}