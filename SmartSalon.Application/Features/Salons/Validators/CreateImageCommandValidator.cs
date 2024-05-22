
using FluentValidation;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.Features.Salons.Commands;

namespace SmartSalon.Application.Features.Salons.Validators;

internal class AddImageCommandValidator : AbstractValidator<CreateImageCommand>
{
    public AddImageCommandValidator()
    {
        RuleFor(command => command.Url).NotEmpty();
        RuleFor(command => command.SalonId).MustBeValidGuid();
    }
}