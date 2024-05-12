
using FluentValidation;
using SmartSalon.Application.Features.Salons.Commands;
using static SmartSalon.Application.ApplicationConstants.Validation.Salon;

namespace SmartSalon.Application.Features.Salons.Validators;

internal class CreateSalonCommandValidator : AbstractValidator<CreateSalonCommand>
{
    public CreateSalonCommandValidator()
    {
        RuleFor(command => command.Name).MaximumLength(MaxNameLength);
        RuleFor(command => command.Location).MaximumLength(MaxGoogleMapsLocationLength);
    }
}