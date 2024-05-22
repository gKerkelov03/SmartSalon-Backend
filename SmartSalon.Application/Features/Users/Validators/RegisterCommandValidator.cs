using FluentValidation;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.Features.Users.Commands;
using static SmartSalon.Application.ApplicationConstants.Validation.User;

namespace SmartSalon.Application.Features.Users.Validators;

internal class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(command => command.Password).MustBeValidPassword();

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