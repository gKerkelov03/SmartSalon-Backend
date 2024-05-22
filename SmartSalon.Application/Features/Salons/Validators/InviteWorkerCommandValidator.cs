using FluentValidation;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.Features.Salons.Commands;

namespace SmartSalon.Application.Features.Salons.Validators;

internal class InviteWorkerCommandValidator : AbstractValidator<InviteWorkerCommand>
{
    public InviteWorkerCommandValidator()
    {
        RuleFor(command => command.WorkerId).MustBeValidGuid();
        RuleFor(command => command.SalonId).MustBeValidGuid();
    }
}