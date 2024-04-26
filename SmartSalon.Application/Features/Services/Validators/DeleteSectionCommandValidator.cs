
using FluentValidation;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.Features.Services.Commands;

namespace SmartSalon.Application.Features.Services.Validators;

internal class DeleteSectionCommandValidator : AbstractValidator<DeleteSectionCommand>
{
    public DeleteSectionCommandValidator()
    {
        RuleFor(command => command.SectionId).MustBeValidGuid();
    }
}