using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain.Bookings;
using SmartSalon.Application.Domain.Salons;
using SmartSalon.Application.Domain.Services;
using SmartSalon.Application.Domain.Users;
using SmartSalon.Application.Errors;
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
    IEfRepository<Service> _services,
    IEfRepository<Customer> _customers,
    IEfRepository<Worker> _workers,
    IEfRepository<Salon> _salons,
    IUnitOfWork _unitOfWork,
    IMapper _mapper
)
    : ICommandHandler<CreateBookingCommand, CreateBookingCommandResponse>
{
    public async Task<Result<CreateBookingCommandResponse>> Handle(CreateBookingCommand command, CancellationToken cancellationToken)
    {
        var customerDoesntExist = await _customers.GetByIdAsync(command.CustomerId) is null;

        if (customerDoesntExist)
        {
            return Error.NotFound;
        }

        var salonDoesntExist = await _salons.GetByIdAsync(command.SalonId) is null;

        if (salonDoesntExist)
        {
            return Error.NotFound;
        }

        var service = await _services.All
            .Include(service => service.JobTitles)
            .FirstOrDefaultAsync(service => service.Id == command.ServiceId);

        if (service is null)
        {
            return Error.NotFound;
        }

        var worker = await _workers.All
            .Include(worker => worker.JobTitles)
            .FirstOrDefaultAsync(worker => worker.Id == command.WorkerId);

        if (worker is null)
        {
            return Error.NotFound;
        }

        var workerCannotProvideService = !worker.JobTitles!.Any(
            workerJobTitle => service.JobTitles!.Any(
                serviceJobTitle => workerJobTitle.Id == serviceJobTitle.Id)
        );

        if (workerCannotProvideService)
        {
            return new Error("This worker cannot provide this service");
        }

        var newBooking = _mapper.Map<Booking>(command);

        await _bookings.AddAsync(newBooking);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new CreateBookingCommandResponse(newBooking.Id);
    }
}
