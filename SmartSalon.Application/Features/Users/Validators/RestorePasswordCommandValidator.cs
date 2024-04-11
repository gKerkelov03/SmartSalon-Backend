using FluentValidation;
using SmartSalon.Application.Extensions;
using static SmartSalon.Application.ApplicationConstants.Validation.User;

namespace SmartSalon.Application.Validators;

internal class RestorePasswordCommandValidator : AbstractValidator<RestorePasswordCommand>
{
    public RestorePasswordCommandValidator()
    {
        RuleFor(command => command.UserId).MustBeValidGuid();
        RuleFor(command => command.NewPassword)
            .MinimumLength(MinPasswordLength)
            .Must(password => password.Any(char.IsUpper))
            .Must(password => password.Any(char.IsLower))
            .Must(password => password.Any(char.IsDigit))
            .Must(password => password.Any(char.IsSymbol));
    }
}