using AutoMapper;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain.Bookings;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Bookings.Commands;

public class CreateBookingCommand : ICommand<CreateBookingCommandResponse>, IMapTo<Booking>
{
    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public Id ServiceId { get; set; }
    public Id CustomerId { get; set; }
    public Id SalonId { get; set; }
    public Id WorkerId { get; set; }
}

public class CreateBookingCommandResponse(Id id)
{
    public Id CreatedBookingId => id;
}

internal class CreateBookingCommandHandler(
    IEfRepository<Booking> _bookings,
    IUnitOfWork _unitOfWork,
    IMapper _mapper
)
    : ICommandHandler<CreateBookingCommand, CreateBookingCommandResponse>
{
    public async Task<Result<CreateBookingCommandResponse>> Handle(CreateBookingCommand command, CancellationToken cancellationToken)
    {
        var newBooking = _mapper.Map<Booking>(command);

        await _bookings.AddAsync(newBooking);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new CreateBookingCommandResponse(newBooking.Id);
    }
}
