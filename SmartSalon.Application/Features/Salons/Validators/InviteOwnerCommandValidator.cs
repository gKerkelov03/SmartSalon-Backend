using FluentValidation;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.Features.Salons.Commands;

namespace SmartSalon.Application.Features.Salons.Validators;

internal class InviteOwnerCommandValidator : AbstractValidator<InviteOwnerCommand>
{
    public InviteOwnerCommandValidator()
    {
        RuleFor(command => command.OwnerId).MustBeValidGuid();
        RuleFor(command => command.SalonId).MustBeValidGuid();
    }
}