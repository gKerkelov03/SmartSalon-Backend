
using FluentValidation;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.Features.Services.Commands;

namespace SmartSalon.Application.Features.Bookings.Validators;

internal class CancelBookingCommandValidator : AbstractValidator<CancelBookingCommand>
{
    public CancelBookingCommandValidator() => RuleFor(command => command.BookingId).MustBeValidGuid();
}