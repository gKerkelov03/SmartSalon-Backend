
using FluentValidation;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.Features.Salons.Commands;

namespace SmartSalon.Application.Features.Salons.Validators;

internal class DeleteSalonCommandValidator : AbstractValidator<DeleteSalonCommand>
{
    public DeleteSalonCommandValidator()
    {
        RuleFor(command => command.SalonId).MustBeValidGuid();
    }
}