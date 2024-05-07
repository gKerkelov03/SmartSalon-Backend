using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Domain.Services;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Services.Queries;

public class GetCategoryByIdQuery(Id id) : IQuery<GetCategoryByIdQueryResponse>
{
    public Id CategoryId => id;
}

public class GetCategoryByIdQueryResponse
{
    public Id Id { get; set; }
    public required string Name { get; set; }
    public required int Order { get; set; }
    public Id SalonId { get; set; }
    public Id SectionId { get; set; }
    public required IEnumerable<Id> ServicesIds { get; set; }
}

internal class GetCategoryByIdQueryHandler(IEfRepository<Category> _categories)
    : IQueryHandler<GetCategoryByIdQuery, GetCategoryByIdQueryResponse>
{
    public async Task<Result<GetCategoryByIdQueryResponse>> Handle(GetCategoryByIdQuery query, CancellationToken cancellationToken)
    {
        var queryResponse = await _categories.All
            .Include(category => category.Services)
            .Where(category => category.Id == query.CategoryId)
            .Select(category => new GetCategoryByIdQueryResponse
            {
                Id = category.Id,
                Name = category.Name,
                Order = category.Order,
                SalonId = category.SalonId,
                SectionId = category.SectionId,
                ServicesIds = category.Services!.Select(service => service.Id)
            })
            .FirstOrDefaultAsync();

        if (queryResponse is null)
        {
            return Error.NotFound;
        }

        return queryResponse;
    }
}