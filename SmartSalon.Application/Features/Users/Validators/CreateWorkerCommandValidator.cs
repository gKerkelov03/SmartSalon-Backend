using FluentValidation;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.Features.Users.Commands;
using static SmartSalon.Application.ApplicationConstants.Validation.User;

namespace SmartSalon.Application.Features.Users.Validators;

internal class CreateWorkerCommandValidator : AbstractValidator<CreateWorkerCommand>
{
    public CreateWorkerCommandValidator()
    {
        RuleFor(command => command.SalonId).MustBeValidGuid();

        RuleFor(command => command.Password).MustBeValidPassword();

        RuleFor(command => command.FirstName)
            .NotEmpty()
            .MaximumLength(MaxFirstNameLength);

        RuleFor(command => command.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(MaxEmailLength);

        RuleFor(command => command.LastName)
            .NotEmpty()
            .MaximumLength(MaxLastNameLength);

        RuleFor(command => command.PhoneNumber)
            .NotEmpty()
            .MaximumLength(MaxPhoneNumberLength);

        RuleFor(command => command.Nickname)
            .NotEmpty();

        RuleFor(command => command.JobTitle)
            .NotEmpty();
    }
}