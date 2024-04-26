
using FluentValidation;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.Features.Salons.Commands;

namespace SmartSalon.Application.Features.Salons.Validators;

internal class DeleteSpecialtyCommandValidator : AbstractValidator<DeleteSpecialtyCommand>
{
    public DeleteSpecialtyCommandValidator()
    {
        RuleFor(command => command.SpecialtyId).MustBeValidGuid();
    }
}