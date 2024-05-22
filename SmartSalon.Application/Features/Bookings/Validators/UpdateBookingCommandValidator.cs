
using FluentValidation;
using SmartBooking.Application.Features.Bookings.Commands;
using SmartSalon.Application.Extensions;

namespace SmartSalon.Application.Features.Bookings.Validators;

internal class UpdateBookingCommandValidator : AbstractValidator<UpdateBookingCommand>
{
    public UpdateBookingCommandValidator()
    {
        RuleFor(query => query.BookingId).MustBeValidGuid();
        RuleFor(query => query.WorkerId).MustBeValidGuid();
        RuleFor(query => query.Date).NotEmpty();
        RuleFor(query => query.StartTime).NotEmpty();
        RuleFor(query => query.EndTime).NotEmpty();
        RuleFor(query => query.Done);
        RuleFor(query => query.Note).NotNull();
    }
}