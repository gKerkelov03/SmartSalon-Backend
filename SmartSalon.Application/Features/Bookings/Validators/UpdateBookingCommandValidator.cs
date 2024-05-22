
using FluentValidation;
using SmartBooking.Application.Features.Bookings.Commands;
using SmartSalon.Application.Extensions;

namespace SmartSalon.Application.Features.Bookings.Validators;

internal class UpdateBookingCommandValidator : AbstractValidator<UpdateBookingCommand>
{
    public UpdateBookingCommandValidator() => RuleFor(query => query.BookingId).MustBeValidGuid();
}