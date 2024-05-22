
using FluentValidation;
using SmartCategory.Application.Features.Services.Commands;
using SmartSalon.Application.Extensions;
using static SmartSalon.Application.ApplicationConstants.Validation.Category;

namespace SmartSalon.Application.Features.Services.Validators;

internal class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(command => command.CategoryId).MustBeValidGuid();
        RuleFor(command => command.Name).MaximumLength(MaxNameLength);
        RuleFor(command => command.Order).NotEmpty();
    }
}