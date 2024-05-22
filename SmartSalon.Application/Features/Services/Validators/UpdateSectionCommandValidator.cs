
using FluentValidation;
using SmartSection.Application.Features.Services.Commands;
using static SmartSalon.Application.ApplicationConstants.Validation.Salon;

namespace SmartSalon.Application.Features.Services.Validators;

internal class UpdateSectionCommandValidator : AbstractValidator<UpdateSectionCommand>
{
    public UpdateSectionCommandValidator()
    {
        RuleFor(command => command.Name).MaximumLength(MaxNameLength);
    }
}