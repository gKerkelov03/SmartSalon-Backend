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
    public required IEnumerable<CategoryQueryResponse> Categories { get; set; }
}

public class CategoryQueryResponse : IMapFrom<Category>
{
    public required string Name { get; set; }
    public required int Order { get; set; }
    public required IEnumerable<ServiceQueryResponse> Services { get; set; }
}

public class ServiceQueryResponse : IMapFrom<Service>
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required double Price { get; set; }
    public required int DurationInMinutes { get; set; }
    public required int Order { get; set; }
    public required ICollection<GetJobTitleByIdQueryResponse>? JobTitles { get; set; }
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