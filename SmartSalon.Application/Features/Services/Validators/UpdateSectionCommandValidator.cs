
using FluentValidation;
using SmartSalon.Application.Extensions;
using SmartSection.Application.Features.Services.Commands;
using static SmartSalon.Application.ApplicationConstants.Validation.Section;

namespace SmartSalon.Application.Features.Services.Validators;

internal class UpdateSectionCommandValidator : AbstractValidator<UpdateSectionCommand>
{
    public UpdateSectionCommandValidator()
    {
        RuleFor(command => command.Name).MaximumLength(MaxNameLength);
        RuleFor(command => command.PictureUrl).NotEmpty();
        RuleFor(command => command.SectionId).MustBeValidGuid();
    }
}