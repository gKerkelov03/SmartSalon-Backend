
using FluentValidation;
using SmartSalon.Application.Features.Bookings.Commands;

namespace SmartSalon.Application.Features.Salons.Validators;

internal class CreateBookingCommandValidator : AbstractValidator<CreateBookingCommand>
{
    public CreateBookingCommandValidator()
    {
    }
}