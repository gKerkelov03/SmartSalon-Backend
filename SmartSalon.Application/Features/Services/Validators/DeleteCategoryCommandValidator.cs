
using FluentValidation;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.Features.Services.Commands;

namespace SmartSalon.Application.Features.Services.Validators;

internal class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
{
    public DeleteCategoryCommandValidator()
    {
        RuleFor(command => command.CategoryId).MustBeValidGuid();
    }
}