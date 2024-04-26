using FluentValidation;
using static SmartSalon.Application.ApplicationConstants.Validation.User;

namespace SmartSalon.Application.Features.Users.Validators;

internal class RestorePasswordCommandValidator : AbstractValidator<RestorePasswordCommand>
{
    public RestorePasswordCommandValidator()
    {
        RuleFor(command => command.Token).NotEmpty();

        RuleFor(command => command.NewPassword)
            .MinimumLength(MinPasswordLength)
            .Must(password => password.Any(char.IsUpper))
            .Must(password => password.Any(char.IsLower))
            .Must(password => password.Any(char.IsDigit))
            .Must(password => password.Any(char.IsSymbol));
    }
}