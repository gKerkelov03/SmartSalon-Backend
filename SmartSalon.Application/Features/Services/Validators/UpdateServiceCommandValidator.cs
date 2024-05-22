
using FluentValidation;
using SmartSalon.Application.Extensions;
using SmartService.Application.Features.Services.Commands;
using static SmartSalon.Application.ApplicationConstants.Validation.Service;

namespace SmartSalon.Application.Features.Services.Validators;

internal class UpdateServiceCommandValidator : AbstractValidator<UpdateServiceCommand>
{
    public UpdateServiceCommandValidator()
    {
        RuleFor(command => command.Name).MaximumLength(MaxNameLength);
        RuleFor(command => command.Description).MaximumLength(MaxDescriptionLength);
        RuleFor(command => command.ServiceId).MustBeValidGuid();
        RuleFor(command => command.Price).NotEmpty();
        RuleFor(command => command.DurationInMinutes).NotEmpty();
        RuleFor(command => command.Order).NotEmpty();
    }
}