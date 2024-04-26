using FluentValidation;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.Features.Users.Commands;

namespace SmartSalon.Application.Features.Users.Validators;

internal class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(command => command.UserId).MustBeValidGuid();
        RuleFor(command => command.CurrentPassword).MustBeValidPassword();
        RuleFor(command => command.NewPassword).MustBeValidPassword();
    }
}