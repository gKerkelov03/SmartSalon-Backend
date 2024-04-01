using FluentValidation;
using SmartSalon.Application.Features.Users.Commands;

namespace SmartSalon.Application.Features.Users.Validators;

internal class DeleteOwnerCommandValidator : AbstractValidator<DeleteOwnerCommand>
{
    public DeleteOwnerCommandValidator()
    {
    }
}