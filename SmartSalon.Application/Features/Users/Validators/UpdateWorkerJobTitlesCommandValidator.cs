using FluentValidation;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.Features.Users.Commands;

namespace SmartSalon.Application.Features.Users.Validators;

internal class UpdateWorkerJobTitlesCommandValidator : AbstractValidator<UpdateWorkerJobTitlesCommand>
{
    public UpdateWorkerJobTitlesCommandValidator()
    {
        RuleFor(command => command.WorkerId).MustBeValidGuid();
        RuleFor(command => command.JobTitlesIds).MustBeCollectionOfValidGuids();
    }
}