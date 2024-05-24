using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Domain.Services;
using SmartSalon.Application.Errors;
using SmartSalon.Application.Features.Salons.Queries;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Services.Queries;

public class GetServiceByIdQuery(Id id) : IQuery<GetServiceByIdQueryResponse>
{
    public Id ServiceId => id;
}

public class GetServiceByIdQueryResponse : IMapFrom<Service>
{
    public Id Id { get; set; }
    public required string PictureUrl { get; set; }
    public required string Name { get; set; }
    public int Order { get; set; }
    public Id SalonId { get; set; }
    public required IEnumerable<GetCategoryByIdQueryResponse> Categories { get; set; }
}

internal class GetServiceByIdQueryHandler(IEfRepository<Service> _services, IMapper _mapper)
    : IQueryHandler<GetServiceByIdQuery, GetServiceByIdQueryResponse>
{
    public async Task<Result<GetServiceByIdQueryResponse>> Handle(GetServiceByIdQuery query, CancellationToken cancellationToken)
    {
        var service = await _services.GetByIdAsync(query.ServiceId);

        if (service is null)
        {
            return Error.NotFound;
        }

        return _mapper.Map<GetServiceByIdQueryResponse>(service);
    }
}