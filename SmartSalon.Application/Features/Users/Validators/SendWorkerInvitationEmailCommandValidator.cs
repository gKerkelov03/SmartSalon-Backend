using FluentValidation;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.Features.Users.Commands;

namespace SmartSalon.Application.Validators;

internal class SendWorkerInvitationEmailCommandValidator : AbstractValidator<SendWorkerInvitationEmailCommand>
{
    public SendWorkerInvitationEmailCommandValidator()
    {
        RuleFor(command => command.WorkerId).MustBeValidGuid();
        RuleFor(command => command.SalonId).MustBeValidGuid();
    }
}