using Microsoft.EntityFrameworkCore;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Domain.Services;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Services.Queries;

public class GetSectionByIdQuery(Id id) : IQuery<GetSectionByIdQueryResponse>
{
    public Id SectionId => id;
}

public class GetSectionByIdQueryResponse
{
    public Id Id { get; set; }
    public required string PictureUrl { get; set; }
    public required string Name { get; set; }
    public int Order { get; set; }
    public Id SalonId { get; set; }
    public required IEnumerable<Id> CategoriesIds { get; set; }
}

internal class GetSectionByIdQueryHandler(IEfRepository<Section> _sections)
    : IQueryHandler<GetSectionByIdQuery, GetSectionByIdQueryResponse>
{
    public async Task<Result<GetSectionByIdQueryResponse>> Handle(GetSectionByIdQuery query, CancellationToken cancellationToken)
    {
        var queryResponse = await _sections.All
            .Include(section => section.Categories)
            .Where(section => section.Id == query.SectionId)
            .Select(section => new GetSectionByIdQueryResponse
            {
                Id = section.Id,
                Name = section.Name,
                Order = section.Order,
                SalonId = section.SalonId,
                PictureUrl = section.PictureUrl,
                CategoriesIds = section.Categories!.Select(service => service.Id)
            })
            .FirstOrDefaultAsync();

        if (queryResponse is null)
        {
            return Error.NotFound;
        }

        return queryResponse;
    }
}