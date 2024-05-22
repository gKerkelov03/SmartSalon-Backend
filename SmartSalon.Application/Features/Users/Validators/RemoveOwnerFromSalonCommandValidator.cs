using FluentValidation;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.Features.Users.Commands;

namespace SmartSalon.Application.Features.Users.Validators;

internal class RemoveOwnerFromSalonCommandValidator : AbstractValidator<RemoveOwnerFromSalonCommand>
{
    public RemoveOwnerFromSalonCommandValidator()
    {
        RuleFor(command => command.SalonId).MustBeValidGuid();
        RuleFor(command => command.OwnerId).MustBeValidGuid();
    }
}