using FluentValidation;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.Features.Users.Commands;

namespace SmartSalon.Application.Validators;

internal class SendEOwnerInvitationEmailCommandValidator : AbstractValidator<SendOwnerInvitationEmailCommand>
{
    public SendEOwnerInvitationEmailCommandValidator()
    {
        RuleFor(command => command.OwnerId).MustBeValidGuid();
        RuleFor(command => command.SalonId).MustBeValidGuid();
    }
}