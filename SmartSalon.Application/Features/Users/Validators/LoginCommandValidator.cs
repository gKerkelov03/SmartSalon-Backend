using FluentValidation;
using SmartSalon.Application.Features.Users.Commands;
using static SmartSalon.Application.ApplicationConstants.Validation.User;

namespace SmartSalon.Application.Validators;

internal class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(command => command.Password)
            .MinimumLength(MinPasswordLength)
            .Must(password => password.Any(char.IsUpper))
            .Must(password => password.Any(char.IsLower))
            .Must(password => password.Any(char.IsDigit))
            .Must(password => password.Any(char.IsSymbol));

        RuleFor(command => command.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(MaxEmailLength);
    }
}