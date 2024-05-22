
using FluentValidation;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.Features.Salons.Commands;

namespace SmartSalon.Application.Features.Salons.Validators;

internal class AddCurrencyCommandValidator : AbstractValidator<AddCurrencyCommand>
{
    public AddCurrencyCommandValidator()
    {
        RuleFor(command => command.CurrencyId).MustBeValidGuid();
        RuleFor(command => command.SalonId).MustBeValidGuid();
    }
}