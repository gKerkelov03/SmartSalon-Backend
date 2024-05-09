using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Domain.Salons;
using SmartSalon.Application.Domain.Services;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Services.Queries;

public class GetServiceByIdQuery(Id id) : IQuery<GetServiceByIdQueryResponse>
{
    public Id ServiceId => id;
}

public class GetServiceByIdQueryResponse : IMapFrom<Service>
{
    public Id Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required double Price { get; set; }
    public required int DurationInMinutes { get; set; }
    public required int Order { get; set; }
    public Id SalonId { get; set; }
    public Id CategoryId { get; set; }
    public required IEnumerable<Id> JobTitles { get; set; }
}

internal class GetServiceByIdQueryHandler(IEfRepository<Service> _services)
    : IQueryHandler<GetServiceByIdQuery, GetServiceByIdQueryResponse>
{
    public async Task<Result<GetServiceByIdQueryResponse>> Handle(GetServiceByIdQuery query, CancellationToken cancellationToken)
    {
        var queryResponse = await _services.All
            .Include(service => service.JobTitles)
            .Select(service => new GetServiceByIdQueryResponse
            {
                Id = service.Id,
                SalonId = service.SalonId,
                CategoryId = service.CategoryId,
                DurationInMinutes = service.DurationInMinutes,
                Price = service.Price,
                Description = service.Description,
                Name = service.Name,
                Order = service.Order,
                //TODO: think about passing the whole JobTitles instead of just the ids and not only here
                JobTitles = service.JobTitles!.Select(jobTitle => jobTitle.Id),
            })
            .FirstOrDefaultAsync(service => service.Id == query.ServiceId);

        if (queryResponse is null)
        {
            return Error.NotFound;
        }

        return queryResponse;
    }
}