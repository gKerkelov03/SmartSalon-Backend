
using FluentValidation;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.Features.Salons.Commands;

namespace SmartSalon.Application.Features.Salons.Validators;

internal class AddSpecialtyCommandValidator : AbstractValidator<AddSpecialtyCommand>
{
    public AddSpecialtyCommandValidator()
    {
        RuleFor(command => command.Text).NotEmpty();
        RuleFor(command => command.SalonId).MustBeValidGuid();
    }
}