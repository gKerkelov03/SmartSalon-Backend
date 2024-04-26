

using FluentValidation;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.Features.Salons.Commands;

namespace SmartSalon.Application.Features.Salons.Validators;

internal class RemoveCurrencyCommandValidator : AbstractValidator<RemoveCurrencyCommand>
{
    public RemoveCurrencyCommandValidator()
    {
        RuleFor(command => command.SalonId).MustBeValidGuid();
        RuleFor(command => command.CurrencyId).MustBeValidGuid();
    }
}