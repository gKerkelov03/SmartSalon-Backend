
using FluentValidation;
using SmartSalon.Application.Features.Salons.Commands;
using static SmartSalon.Application.ApplicationConstants.Validation.Salon;

namespace SmartSalon.Application.Features.Salons.Validators;

internal class CreateSalonCommandValidator : AbstractValidator<CreateSalonCommand>
{
    public CreateSalonCommandValidator()
    {
        RuleFor(command => command.Name).MaximumLength(MaxNameLength);
        RuleFor(command => command.Description).MaximumLength(MaxDescriptionLength);
        RuleFor(command => command.Location).MaximumLength(MaxLocationLength);
        RuleFor(command => command.ProfilePictureUrl).NotEmpty();
        RuleFor(command => command.ProfilePictureUrl).NotEmpty();
    }
}