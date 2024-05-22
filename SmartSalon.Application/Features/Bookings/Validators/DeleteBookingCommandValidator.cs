
using FluentValidation;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.Features.Services.Commands;

namespace SmartSalon.Application.Features.Bookings.Validators;

internal class DeleteBookingCommandValidator : AbstractValidator<DeleteBookingCommand>
{
    public DeleteBookingCommandValidator() => RuleFor(command => command.BookingId).MustBeValidGuid();
}