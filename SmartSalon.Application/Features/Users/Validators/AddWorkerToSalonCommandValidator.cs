using FluentValidation;
using SmartSalon.Application.Features.Users.Commands;

namespace SmartSalon.Application.Features.Users.Validators;

internal class AddWorkerToSalonCommandValidator : AbstractValidator<AddWorkerToSalonCommand>
{
    public AddWorkerToSalonCommandValidator()
    {
        RuleFor(command => command.Token).NotEmpty();
    }
}