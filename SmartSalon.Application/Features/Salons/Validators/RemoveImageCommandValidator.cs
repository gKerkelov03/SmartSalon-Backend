
using FluentValidation;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.Features.Salons.Commands;

namespace SmartSalon.Application.Validators;

internal class RemoveImageCommandValidator : AbstractValidator<RemoveImageCommand>
{
    public RemoveImageCommandValidator()
    {
        RuleFor(command => command.SalonId).MustBeValidGuid();
        RuleFor(command => command.ImageId).MustBeValidGuid();
    }
}