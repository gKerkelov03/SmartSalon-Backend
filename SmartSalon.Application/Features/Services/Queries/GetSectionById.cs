using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
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
    public required string PictureUrl { get; set; }
    public required string Name { get; set; }
    public int Order { get; set; }
    public Id SalonId { get; set; }
    public required IEnumerable<GetCategoryByIdQueryResponse> Categories { get; set; }
}

internal class GetSectionByIdQueryHandler(IEfRepository<Section> _sections, IMapper _mapper)
    : IQueryHandler<GetSectionByIdQuery, GetSectionByIdQueryResponse>
{
    public async Task<Result<GetSectionByIdQueryResponse>> Handle(GetSectionByIdQuery query, CancellationToken cancellationToken)
    {
        var queryResponse = await _sections.All
            .Include(section => section.Categories)
            !.ThenInclude(category => category.Services)
            !.ThenInclude(service => service.JobTitles)
            .ProjectTo<GetSectionByIdQueryResponse>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(section => section.Id == query.SectionId);

        if (queryResponse is null)
        {
            return Error.NotFound;
        }

        return queryResponse;
    }
}