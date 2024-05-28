using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Abstractions.Services;
using SmartSalon.Application.Domain.Bookings;
using SmartSalon.Application.Errors;
using SmartSalon.Application.Models.Emails;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Services.Commands;

public class DeleteBookingCommand : ICommand
{
    public Id BookingId { get; set; }
}

public class BookingDatabaseQueryResponse : IMapFrom<Booking>
{
    public Id Id { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public required string SalonName { get; set; }
    public required string CustomerFirstName { get; set; }
    public required string CustomerEmail { get; set; }
    public required string ServiceName { get; set; }
}

internal class DeleteBookingCommandHandler(
    IEfRepository<Booking> _bookings,
    IUnitOfWork _unitOfWork,
    IMapper _mapper,
    IEmailsManager _emailsManager
)
    : ICommandHandler<DeleteBookingCommand>
{
    public async Task<Result> Handle(DeleteBookingCommand command, CancellationToken cancellationToken)
    {
        var booking = await _bookings.All
            .Include(booking => booking.Salon)
            .Include(booking => booking.Service)
            .Include(booking => booking.Customer)
            .ProjectTo<BookingDatabaseQueryResponse>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(booking => booking.Id == command.BookingId);

        if (booking is null)
        {
            return Error.NotFound;
        }

        var viewModel = new BookingCancellationViewModel()
        {
            ServiceName = booking.ServiceName,
            SalonName = booking.SalonName,
            UserFirstName = booking.CustomerFirstName,
        };

        await _bookings.RemoveByIdAsync(booking.Id);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        await _emailsManager.SendBookingCancellationEmailAsync(booking.CustomerEmail, viewModel);

        return Result.Success();
    }
}
