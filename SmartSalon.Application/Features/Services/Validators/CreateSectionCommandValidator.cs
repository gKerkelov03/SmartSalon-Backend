
using FluentValidation;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.Features.Services.Commands;
using static SmartSalon.Application.ApplicationConstants.Validation.Section;

namespace SmartSalon.Application.Features.Services.Validators;

internal class CreateSectionCommandValidator : AbstractValidator<CreateSectionCommand>
{
    public CreateSectionCommandValidator()
    {
        RuleFor(command => command.SalonId).MustBeValidGuid();
        RuleFor(command => command.Name).MaximumLength(MaxNameLength);
        RuleFor(command => command.PictureUrl).NotEmpty();
    }
}