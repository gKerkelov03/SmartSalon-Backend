using FluentValidation;
using SmartSalon.Application.Features.Users.Commands;
using SmartSalon.Application.Extensions;
using static SmartSalon.Application.ApplicationConstants.Validation;

namespace SmartSalon.Application.Features.Users.Validators;

internal class ChangeEmailCommandValidator : AbstractValidator<ChangeEmailCommand>
{
    public ChangeEmailCommandValidator()
    {
        RuleFor(command => command.NewEmail).EmailAddress();
        RuleFor(command => command.UserId).MustBeValidGuid();

        RuleFor(command => command.Password)
            .MinimumLength(User.MinPasswordLength)
            .Must(password => password.Any(char.IsUpper))
            .Must(password => password.Any(char.IsLower))
            .Must(password => password.Any(char.IsDigit))
            .Must(password => password.Any(char.IsSymbol));
    }
}