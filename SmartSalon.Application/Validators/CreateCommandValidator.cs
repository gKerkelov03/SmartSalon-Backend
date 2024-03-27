using FluentValidation;
using SmartSalon.Application.Commands;

namespace SmartSalon.Application.Validators;

internal class CreateCommandValidator : AbstractValidator<CreateCommand>
{
    public CreateCommandValidator()
    {
        RuleFor(command => command.Age).NotEmpty().GreaterThan(5);
        RuleFor(command => command.Name).NotNull().NotEmpty().Length(10, 15);
    }
}