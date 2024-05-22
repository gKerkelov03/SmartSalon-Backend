
using FluentValidation;
using SmartSalon.Application.Features.Services.Commands;
using static SmartSalon.Application.ApplicationConstants.Validation.Category;

namespace SmartSalon.Application.Features.Services.Validators;

internal class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(command => command.Name).MaximumLength(MaxNameLength);
    }
}