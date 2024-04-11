using FluentValidation;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.Features.Users.Commands;
using static SmartSalon.Application.ApplicationConstants.Validation.User;

namespace SmartSalon.Application.Validators;

internal class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(command => command.UserId).MustBeValidGuid();

        RuleFor(command => command.FirstName)
            .NotEmpty()
            .MaximumLength(MaxFirstNameLength);

        RuleFor(command => command.LastName)
            .NotEmpty()
            .MaximumLength(MaxLastNameLength);

        RuleFor(command => command.PhoneNumber)
            .NotEmpty()
            .MaximumLength(MaxPhoneNumberLength);
    }
}