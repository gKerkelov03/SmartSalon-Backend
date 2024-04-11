using FluentValidation;
using SmartSalon.Application.Features.Users.Commands;
using static SmartSalon.Application.ApplicationConstants.Validation.User;

namespace SmartSalon.Application.Validators;

internal class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(command => command.Password)
            .MinimumLength(MinPasswordLength)
            .Must(password => password.Any(char.IsUpper))
            .Must(password => password.Any(char.IsLower))
            .Must(password => password.Any(char.IsDigit))
            .Must(password => password.Any(char.IsSymbol));

        RuleFor(command => command.FirstName)
            .NotEmpty()
            .MaximumLength(MaxFirstNameLength);

        RuleFor(command => command.Email)
            .EmailAddress()
            .MaximumLength(MaxEmailLength);

        RuleFor(command => command.LastName)
            .NotEmpty()
            .MaximumLength(MaxLastNameLength);

        RuleFor(command => command.PhoneNumber)
            .NotEmpty()
            .MaximumLength(MaxPhoneNumberLength);
    }
}