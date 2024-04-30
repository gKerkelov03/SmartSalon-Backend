
using FluentValidation;
using SmartBooking.Application.Features.Bookings.Commands;

namespace SmartSalon.Application.Features.Bookings.Validators;

internal class UpdateBookingCommandValidator : AbstractValidator<UpdateBookingCommand>
{
    public UpdateBookingCommandValidator()
    {
    }
}