using FluentValidation;
using SmartSalon.Application.Commands;

namespace SmartSalon.Application.Validators;

internal class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
    }
}