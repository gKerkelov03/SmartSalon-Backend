using FluentValidation;
using SmartSalon.Application.Features.Users.Commands;

namespace SmartSalon.Application.Features.Users.Validators;

internal class DeleteWorkerCommandValidator : AbstractValidator<DeleteWorkerCommand>
{
    public DeleteWorkerCommandValidator()
    {
    }
}