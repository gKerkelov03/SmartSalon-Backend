
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain.Bookings;
using SmartSalon.Application.Errors;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.ResultObject;

namespace SmartBooking.Application.Features.Bookings.Commands;

public class UpdateBookingCommand : ICommand
{
    public Id BookingId { get; set; }
    public Id WorkerId { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly From { get; set; }
    public TimeOnly To { get; set; }
    public bool Done { get; set; }
    public required string Note { get; set; }
}

internal class UpdateBookingCommandHandler(IEfRepository<Booking> _bookings, IUnitOfWork _unitOfWork)
    : ICommandHandler<UpdateBookingCommand>
{
    public async Task<Result> Handle(UpdateBookingCommand command, CancellationToken cancellationToken)
    {
        var booking = await _bookings.GetByIdAsync(command.BookingId);

        if (booking is null)
        {
            return Error.NotFound;
        }

        booking.MapAgainst(command);
        _bookings.Update(booking);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
