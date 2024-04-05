using FluentValidation;
using SmartSalon.Application.Features.Users.Commands;

namespace SmartSalon.Application.Features.Users.Validators;

internal class CreateOwnerCommandValidator : AbstractValidator<AddOwnerToSalonCommand>
{
    public CreateOwnerCommandValidator()
    {
    }
}