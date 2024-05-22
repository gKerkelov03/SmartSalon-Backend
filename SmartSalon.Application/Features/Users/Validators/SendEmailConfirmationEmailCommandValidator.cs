using FluentValidation;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.Features.Users.Commands;

namespace SmartSalon.Application.Features.Users.Validators;

internal class SendEmailConfirmationEmailCommandValidator : AbstractValidator<SendEmailConfirmationEmailCommand>
{
    public SendEmailConfirmationEmailCommandValidator()
    {
        RuleFor(command => command.EmailToBeConfirmed).EmailAddress();
        RuleFor(command => command.UserId).MustBeValidGuid();
        RuleFor(command => command.Password).MustBeValidPassword();
    }
}