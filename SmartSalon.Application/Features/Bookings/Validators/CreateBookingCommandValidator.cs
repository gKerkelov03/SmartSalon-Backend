﻿
using FluentValidation;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.Features.Bookings.Commands;

namespace SmartSalon.Application.Features.Salons.Validators;

internal class CreateBookingCommandValidator : AbstractValidator<CreateBookingCommand>
{
    public CreateBookingCommandValidator()
    {
        RuleFor(command => command.Date).NotEmpty();
        RuleFor(command => command.StartTime).NotEmpty();
        RuleFor(command => command.EndTime).NotEmpty();
        RuleFor(command => command.ServiceId).MustBeValidGuid();
        RuleFor(command => command.CustomerId).MustBeValidGuid();
        RuleFor(command => command.SalonId).MustBeValidGuid();
        RuleFor(command => command.WorkerId).MustBeValidGuid();
    }
}