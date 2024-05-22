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
    public Id Id { get; set; }
    public required string Url { get; set; }
    public Id SalonId { get; set; }
}

internal class GetCategoryByIdQueryHandler(IEfRepository<Category> _category, IMapper _mapper)
    : IQueryHandler<GetCategoryByIdQuery, GetCategoryByIdQueryResponse>
{
    public async Task<Result<GetCategoryByIdQueryResponse>> Handle(GetCategoryByIdQuery query, CancellationToken cancellationToken)
    {
        var service = await _category.GetByIdAsync(query.CategoryId);

        if (service is null)
        {
            return Error.NotFound;
        }

        return _mapper.Map<GetCategoryByIdQueryResponse>(service);
    }
}