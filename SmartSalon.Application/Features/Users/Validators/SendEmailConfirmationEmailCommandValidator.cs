using FluentValidation;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.Features.Users.Commands;

namespace SmartSalon.Application.Validators;

internal class SendEmailConfirmationEmailCommandValidator : AbstractValidator<SendEmailConfirmationEmailCommand>
{
    public SendEmailConfirmationEmailCommandValidator()
    {
        RuleFor(command => command.UserId).MustBeValidGuid();
    }
}