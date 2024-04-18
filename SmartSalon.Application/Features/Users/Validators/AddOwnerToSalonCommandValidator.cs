using FluentValidation;
using SmartSalon.Application.Features.Users.Commands;

namespace SmartSalon.Application.Features.Users.Validators;

internal class AddOwnerToSalonCommandValidator : AbstractValidator<AddOwnerToSalonCommand>
{
    public AddOwnerToSalonCommandValidator()
    {
        RuleFor(command => command.Token).NotEmpty();
    }
}