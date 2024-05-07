﻿
using FluentValidation;
using SmartSalon.Application.Extensions;
using SmartWorkingTime.Application.Features.Salons.Commands;

namespace SmartSalon.Application.Features.Salons.Validators;

internal class UpdateWorkingTimeCommandValidator : AbstractValidator<UpdateWorkingTimeCommand>
{
    public UpdateWorkingTimeCommandValidator()
    {
        RuleFor(command => command.WorkingTimeId).MustBeValidGuid();
        RuleFor(command => command.DayOfWeek).NotNull();
        RuleFor(command => command.StartTime).NotNull();
        RuleFor(command => command.EndTime).NotNull();
    }
}