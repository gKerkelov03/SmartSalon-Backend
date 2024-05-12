using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Domain.Salons;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Salons.Queries;

public class GetSalonByIdQuery(Id id) : IQuery<GetSalonByIdQueryResponse>
{
    public Id SalonId => id;
}

public class GetSalonByIdQueryResponse : IHaveCustomMapping
{
    public Id Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string GoogleMapsLocation { get; set; }
    public required string Country { get; set; }
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

    public void CreateMapping(IProfileExpression config)
        => config
            .CreateMap<Salon, GetSalonByIdQueryResponse>()
            .ForMember(
                destination => destination.AcceptedCurrencies,
                options => options.MapFrom(source => source.AcceptedCurrencies!.Select(currency => currency.Id))
            )
            .ForMember(
                destination => destination.Owners,
                options => options.MapFrom(source => source.Owners!.Select(owners => owners.Id))
            )
            .ForMember(
                destination => destination.Workers,
                options => options.MapFrom(source => source.Workers!.Select(workers => workers.Id))
            )
            .ForMember(
                destination => destination.Specialties,
                options => options.MapFrom(source => source.Specialties!.Select(specialty => specialty.Id))
            )
            .ForMember(
                destination => destination.Sections,
                options => options.MapFrom(source => source.Sections!.Select(section => section.Id))
            )
            .ForMember(
                destination => destination.Images,
                options => options.MapFrom(source => source.Images!.Select(image => image.Id))
            )
            .ForMember(
                destination => destination.JobTitles,
                options => options.MapFrom(source => source.JobTitles!.Select(jobTitle => jobTitle.Id))
            );
}

internal class GetSalonByIdQueryHandler(IEfRepository<Salon> _salons, IMapper _mapper)
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
            .ProjectTo<GetSalonByIdQueryResponse>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(salon => salon.Id == query.SalonId);

        if (queryResponse is null)
        {
            return Error.NotFound;
        }

        return queryResponse;
    }
}
