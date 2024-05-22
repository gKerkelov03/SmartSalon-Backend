using FluentValidation;
using SmartSalon.Application.Extensions;

namespace SmartSalon.Application.Features.Users.Validators;

internal class RestorePasswordCommandValidator : AbstractValidator<RestorePasswordCommand>
{
    public RestorePasswordCommandValidator()
    {
        RuleFor(command => command.Token).NotEmpty();
        RuleFor(command => command.NewPassword).MustBeValidPassword();
    }
}