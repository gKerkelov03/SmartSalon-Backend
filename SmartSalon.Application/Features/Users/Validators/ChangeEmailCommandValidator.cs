using FluentValidation;
using SmartSalon.Application.Features.Users.Commands;
using SmartSalon.Application.Extensions;

namespace SmartSalon.Application.Features.Users.Validators;

internal class ChangeEmailCommandValidator : AbstractValidator<ChangeEmailCommand>
{
    public ChangeEmailCommandValidator()
    {
        RuleFor(command => command.NewEmail).EmailAddress();
        RuleFor(command => command.UserId).MustBeValidGuid();
        RuleFor(command => command.Password).MustBeValidPassword();
    }
}