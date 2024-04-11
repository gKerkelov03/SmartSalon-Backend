using FluentValidation;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.Features.Users.Commands;
using static SmartSalon.Application.ApplicationConstants.Validation.User;

namespace SmartSalon.Application.Features.Users.Validators;

internal class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(command => command.UserId).MustBeValidGuid();

        RuleFor(command => command.CurrentPassword)
            .MinimumLength(MinPasswordLength)
            .Must(password => password.Any(char.IsUpper))
            .Must(password => password.Any(char.IsLower))
            .Must(password => password.Any(char.IsDigit))
            .Must(password => password.Any(char.IsSymbol));

        RuleFor(command => command.NewPassword)
            .MinimumLength(MinPasswordLength)
            .Must(password => password.Any(char.IsUpper))
            .Must(password => password.Any(char.IsLower))
            .Must(password => password.Any(char.IsDigit))
            .Must(password => password.Any(char.IsSymbol));
    }
}