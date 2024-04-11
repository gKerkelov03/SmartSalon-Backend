using FluentValidation;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.Features.Users.Commands;
using static SmartSalon.Application.ApplicationConstants.Validation.User;

namespace SmartSalon.Application.Features.Users.Validators;

internal class CreateOwnerCommandValidator : AbstractValidator<CreateOwnerCommand>
{
    public CreateOwnerCommandValidator()
    {
        RuleFor(command => command.SalonId).MustBeValidGuid();

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
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(MaxEmailLength);

        RuleFor(command => command.LastName)
            .NotEmpty()
            .MaximumLength(MaxLastNameLength); ;

        RuleFor(command => command.PhoneNumber)
            .NotEmpty()
            .MaximumLength(MaxPhoneNumberLength); ;

        RuleFor(command => command.ProfilePictureUrl)
            .NotEmpty();
    }
}