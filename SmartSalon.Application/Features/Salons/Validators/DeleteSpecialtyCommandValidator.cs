
using FluentValidation;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.Features.Salons.Commands;

namespace SmartSalon.Application.Validators;

internal class DeleteSpecialtyCommandValidator : AbstractValidator<DeleteSpecialtyCommand>
{
    public DeleteSpecialtyCommandValidator()
    {
        RuleFor(command => command.SpecialtyId).MustBeValidGuid();
    }
}