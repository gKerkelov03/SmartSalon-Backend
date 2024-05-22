
using FluentValidation;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.Features.Services.Commands;

namespace SmartSalon.Application.Features.Services.Validators;

internal class CreateServiceCommandValidator : AbstractValidator<CreateServiceCommand>
{
    public CreateServiceCommandValidator()
    {
        RuleFor(command => command.SalonId).MustBeValidGuid();
    }
}