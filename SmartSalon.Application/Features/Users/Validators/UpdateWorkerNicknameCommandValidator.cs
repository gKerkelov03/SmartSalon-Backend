using FluentValidation;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.Features.Users.Commands;
using static SmartSalon.Application.ApplicationConstants.Validation.Worker;

namespace SmartSalon.Application.Features.Users.Validators;

internal class UpdateWorkerNicknameCommandValidator : AbstractValidator<UpdateWorkerNicknameCommand>
{
    public UpdateWorkerNicknameCommandValidator()
    {
        RuleFor(command => command.WorkerId).MustBeValidGuid();

        RuleFor(command => command.Nickname)
            .NotEmpty()
            .MaximumLength(MaxNicknameLength);
    }
}