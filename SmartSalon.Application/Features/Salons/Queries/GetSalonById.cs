using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Domain.Salons;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Salons.Queries;

public class GetSalonByIdQuery(Id id) : IQuery<GetSalonByIdQueryResponse>
{
    public Id SalonId => id;
}

public class GetSalonByIdQueryResponse
{
    public Id Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Location { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public required int TimePenalty { get; set; }
    public required int BookingsInAdvance { get; set; }
    public bool SubscriptionsEnabled { get; set; }
    public bool SectionsEnabled { get; set; }
    public bool WorkersCanMoveBookings { get; set; }
    public bool WorkersCanSetNonWorkingPeriods { get; set; }
    public Id WorkingTimeId { get; set; }
    public Id MainCurrencyId { get; set; }
    public required IEnumerable<Id> AcceptedCurrencies { get; set; }
    public required IEnumerable<Id> Owners { get; set; }
    public required IEnumerable<Id> Workers { get; set; }
    public required IEnumerable<Id> Specialties { get; set; }
    public required IEnumerable<Id> Sections { get; set; }
    public required IEnumerable<Id> Images { get; set; }
    public required IEnumerable<Id> JobTitles { get; set; }
}

internal class GetSalonByIdQueryHandler(IEfRepository<Salon> _salons)
    : IQueryHandler<GetSalonByIdQuery, GetSalonByIdQueryResponse>
{
    public async Task<Result<GetSalonByIdQueryResponse>> Handle(GetSalonByIdQuery query, CancellationToken cancellationToken)
    {
        var queryResponse = await _salons.All
            .Include(salon => salon.Workers)
            .Include(salon => salon.Owners)
            .Include(salon => salon.AcceptedCurrencies)
            .Include(salon => salon.Sections)
            .Include(salon => salon.Images)
            .Include(salon => salon.Specialties)
            .Include(salon => salon.JobTitles)
            .Where(salon => salon.Id == query.SalonId)
            .Select(salon => new GetSalonByIdQueryResponse
            {
                Id = salon.Id,
                Name = salon.Name,
                Description = salon.Description,
                Location = salon.Location,
                ProfilePictureUrl = salon.ProfilePictureUrl,
                TimePenalty = salon.TimePenalty,
                BookingsInAdvance = salon.BookingsInAdvance,
                SubscriptionsEnabled = salon.SubscriptionsEnabled,
                WorkersCanMoveBookings = salon.WorkersCanMoveBookings,
                WorkersCanSetNonWorkingPeriods = salon.WorkersCanSetNonWorkingPeriods,
                WorkingTimeId = salon.WorkingTimeId,
                MainCurrencyId = salon.MainCurrencyId,
                AcceptedCurrencies = salon.AcceptedCurrencies!.Select(currency => currency.Id),
                Owners = salon.Owners!.Select(owners => owners.Id),
                Workers = salon.Workers!.Select(workers => workers.Id),
                Specialties = salon.Specialties!.Select(specialty => specialty.Id),
                Sections = salon.Sections!.Select(section => section.Id),
                Images = salon.Images!.Select(image => image.Id),
                JobTitles = salon.JobTitles!.Select(jobTitle => jobTitle.Id)
            })
            .FirstOrDefaultAsync();

        if (queryResponse is null)
        {
            return Error.NotFound;
        }

        return queryResponse;
    }
}
