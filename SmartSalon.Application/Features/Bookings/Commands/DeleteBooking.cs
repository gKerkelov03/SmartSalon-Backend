using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain.Bookings;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Services.Commands;

public class DeleteBookingCommand : ICommand
{
    public Id BookingId { get; set; }
}

internal class DeleteBookingCommandHandler(IEfRepository<Booking> _bookings, IUnitOfWork _unitOfWork)
    : ICommandHandler<DeleteBookingCommand>
{
    public async Task<Result> Handle(DeleteBookingCommand command, CancellationToken cancellationToken)
    {
        var booking = await _bookings.GetByIdAsync(command.BookingId);

        if (booking is null)
        {
            return Error.NotFound;
        }

        await _bookings.RemoveByIdAsync(booking.Id);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
