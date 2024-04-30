
using FluentValidation;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.Features.Services.Commands;
using static SmartSalon.Application.ApplicationConstants.Validation.Service;

namespace SmartSalon.Application.Features.Services.Validators;

internal class CreateServiceCommandValidator : AbstractValidator<CreateServiceCommand>
{
    public CreateServiceCommandValidator()
    {
        RuleFor(command => command.CategoryId).MustBeValidGuid();
        RuleFor(command => command.Name).MaximumLength(MaxNameLength);
        RuleFor(command => command.Description).MaximumLength(MaxDescriptionLength);
        RuleFor(command => command.DurationInMinutes).NotEmpty();
        RuleFor(command => command.Price).NotEmpty();
    }
}