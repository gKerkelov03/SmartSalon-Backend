using FluentValidation;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.Features.Users.Commands;
using static SmartSalon.Application.ApplicationConstants.Validation.Worker;

namespace SmartSalon.Application.Features.Users.Validators;

internal class UpdateWorkerCommandValidator : AbstractValidator<UpdateWorkerCommand>
{
    public UpdateWorkerCommandValidator()
    {
        RuleFor(command => command.WorkerId).MustBeValidGuid();

        RuleFor(command => command.JobTitle)
            .NotEmpty()
            .MaximumLength(MaxJobTitleLength);

        RuleFor(command => command.Nickname)
            .NotEmpty()
            .MaximumLength(MaxNicknameLength);
    }
}