
using FluentValidation;
using SmartCategory.Application.Features.Services.Commands;

namespace SmartSalon.Application.Features.Services.Validators;

internal class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
    }
}