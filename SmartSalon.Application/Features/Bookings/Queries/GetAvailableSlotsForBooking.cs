using System.Security.Cryptography;
using System.Security.Principal;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Domain.Bookings;
using SmartSalon.Application.Domain.Salons;
using SmartSalon.Application.Domain.Services;
using SmartSalon.Application.Domain.Users;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Services.Queries;

public class GetAvailableSlotsForBookingQuery : IQuery<IEnumerable<Slot>>
{
    public DateOnly Date { get; set; }
    public Id ServiceId { get; set; }
    public Id CustomerId { get; set; }
    public Id WorkerId { get; set; }
    public Id SalonId { get; set; }
}

public class Slot(TimeOnly from, TimeOnly to)
{
    public TimeOnly From => from;
    public TimeOnly To => to;
}

internal class GetAvailableSlotsForBookingQueryHandler(
    IEfRepository<Worker> _workers,
    IEfRepository<Service> _services,
    IEfRepository<Salon> _salons,
    IEfRepository<Customer> _customers,
    IEfRepository<Booking> _bookings
) : IQueryHandler<GetAvailableSlotsForBookingQuery, IEnumerable<Slot>>
{
    public async Task<Result<IEnumerable<Slot>>> Handle(GetAvailableSlotsForBookingQuery query, CancellationToken cancellationToken)
    {
        var worker = await _workers.GetByIdAsync(query.WorkerId);
        if (worker is null)
        {
            return Error.NotFound;
        }

        var service = await _services.GetByIdAsync(query.ServiceId);
        if (service is null)
        {
            return Error.NotFound;
        }

        var customer = await _customers.GetByIdAsync(query.CustomerId);
        if (customer is null)
        {
            return Error.NotFound;
        }

        var salon = await _salons.All
            .Include(salon => salon.WorkingTime)
            .FirstOrDefaultAsync(salon => salon.Id == query.SalonId);

        if (salon is null)
        {
            return Error.NotFound;
        }

        var (openingTime, closingTime) = GetWorkingTimeForDate(salon.WorkingTime!, query.Date.DayOfWeek);

        var customerTakenSlots = _bookings.All
            .Where(booking => booking.CustomerId == query.CustomerId && !booking.Done)
            .ToList()
            .Select(booking => new Slot(booking.StartTime, booking.EndTime));

        var workerTakenSlots = _bookings.All.Where(booking =>
            booking.WorkerId == query.WorkerId &&
            !booking.Done &&
            booking.Date == query.Date
        )
        .ToList()
        .Select(booking => new Slot(booking.StartTime, booking.EndTime));

        var cursor = openingTime;
        var availableSlots = new List<Slot>();

        while (cursor.IsBetween(openingTime, closingTime))
        {
            var endTimeForCurrentCursor = cursor.AddMinutes(service.DurationInMinutes);

            var customerIsAvailable = !customerTakenSlots.Any(slot =>
               cursor.Hour >= slot.From.Hour && cursor.Minute >= slot.From.Minute &&
               cursor.Hour <= slot.To.Hour && cursor.Minute <= slot.To.Minute ||

               endTimeForCurrentCursor.Hour >= slot.From.Hour && endTimeForCurrentCursor.Minute >= slot.From.Minute &&
               endTimeForCurrentCursor.Hour <= slot.To.Hour && endTimeForCurrentCursor.Minute <= slot.To.Minute
            );

            var workerIsAvailable = !workerTakenSlots.Any(slot =>
               cursor.Hour >= slot.From.Hour && cursor.Minute >= slot.From.Minute &&
               cursor.Hour <= slot.To.Hour && cursor.Minute <= slot.To.Minute ||

               endTimeForCurrentCursor.Hour >= slot.From.Hour && endTimeForCurrentCursor.Minute >= slot.From.Minute &&
               endTimeForCurrentCursor.Hour <= slot.To.Hour && endTimeForCurrentCursor.Minute <= slot.To.Minute
            );

            if (customerIsAvailable && workerIsAvailable)
            {
                availableSlots.Add(new Slot(cursor, endTimeForCurrentCursor));
            }

            cursor = endTimeForCurrentCursor;
        }

        return availableSlots;
    }

    private (TimeOnly openingTime, TimeOnly closingTime) GetWorkingTimeForDate(WorkingTime workingTime, DayOfWeek day)
        => day switch
        {
            DayOfWeek.Monday => (workingTime.MondayOpeningTime, workingTime.MondayClosingTime),
            DayOfWeek.Tuesday => (workingTime.TuesdayOpeningTime, workingTime.TuesdayClosingTime),
            DayOfWeek.Wednesday => (workingTime.WednesdayOpeningTime, workingTime.WednesdayClosingTime),
            DayOfWeek.Thursday => (workingTime.ThursdayOpeningTime, workingTime.ThursdayClosingTime),
            DayOfWeek.Friday => (workingTime.FridayOpeningTime, workingTime.FridayClosingTime),
            DayOfWeek.Saturday => (workingTime.SaturdayOpeningTime, workingTime.SaturdayClosingTime),
            DayOfWeek.Sunday => (workingTime.SundayOpeningTime, workingTime.SundayClosingTime),
            _ => throw new ArgumentOutOfRangeException()
        };
}