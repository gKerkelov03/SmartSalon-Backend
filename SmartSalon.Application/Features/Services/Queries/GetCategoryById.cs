using AutoMapper;
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

public class GetCategoryByIdQueryResponse : IMapFrom<Category>
{
    public required Id Id { get; set; }
    public required string Name { get; set; }
    public required int Order { get; set; }
    public required IEnumerable<GetServiceByIdQueryResponse> Services { get; set; }
}

internal class GetCategoryByIdQueryHandler(IEfRepository<Category> _categories, IMapper _mapper)
    : IQueryHandler<GetCategoryByIdQuery, GetCategoryByIdQueryResponse>
{
    public async Task<Result<GetCategoryByIdQueryResponse>> Handle(GetCategoryByIdQuery query, CancellationToken cancellationToken)
    {
        var category = await _categories.GetByIdAsync(query.CategoryId);

        if (category is null)
        {
            return Error.NotFound;
        }

        return _mapper.Map<GetCategoryByIdQueryResponse>(category);
    }
}