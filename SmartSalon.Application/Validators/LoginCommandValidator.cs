using FluentValidation;
using SmartSalon.Application.Commands;

namespace SmartSalon.Application.Validators;

internal class CreateCommandValidator : AbstractValidator<CreateWorkerCommand>
{
    public CreateCommandValidator()
    {
        RuleFor(command => command.UserName).NotEmpty();
        RuleFor(command => command.FirstName).NotNull().NotEmpty().Length(10, 15);
    }
}