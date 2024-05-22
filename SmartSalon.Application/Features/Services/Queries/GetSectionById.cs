using AutoMapper;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Domain.Services;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Services.Queries;

public class GetSectionByIdQuery(Id id) : IQuery<GetSectionByIdQueryResponse>
{
    public Id SectionId => id;
}

public class GetSectionByIdQueryResponse : IMapFrom<Section>
{
    public Id Id { get; set; }
    public required string Url { get; set; }
    public Id SalonId { get; set; }
}

internal class GetSectionByIdQueryHandler(IEfRepository<Section> _services, IMapper _mapper)
    : IQueryHandler<GetSectionByIdQuery, GetSectionByIdQueryResponse>
{
    public async Task<Result<GetSectionByIdQueryResponse>> Handle(GetSectionByIdQuery query, CancellationToken cancellationToken)
    {
        var service = await _services.GetByIdAsync(query.SectionId);

        if (service is null)
        {
            return Error.NotFound;
        }

        return _mapper.Map<GetSectionByIdQueryResponse>(service);
    }
}