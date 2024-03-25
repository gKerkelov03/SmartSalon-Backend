using FluentValidation;
using SmartSalon.Application.Queries;

namespace SmartSalon.Application.Validators;

internal class CreateCommandValidator : AbstractValidator<CreateCommand>
{
    public CreateCommandValidator()
    {
        RuleFor(command => command.Age).GreaterThan(5);
        RuleFor(command => command.Name).Length(10, 15);
    }
}