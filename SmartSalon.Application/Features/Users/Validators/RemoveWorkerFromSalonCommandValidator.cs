using FluentValidation;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.Features.Users.Commands;

namespace SmartSalon.Application.Features.Users.Validators;

internal class RemoveWorkerFromSalonCommandValidator : AbstractValidator<RemoveWorkerFromSalonCommand>
{
    public RemoveWorkerFromSalonCommandValidator()
    {
        RuleFor(command => command.WorkerId).MustBeValidGuid();
    }
}