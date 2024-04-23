using AutoMapper;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Domain.Salons;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Salons.Queries;

public class GetImageByIdQuery(Id id) : IQuery<GetImageByIdQueryResponse>
{
    public Id ImageId => id;
}

public class GetImageByIdQueryResponse : IMapFrom<Image>
{
    public required string Text { get; set; }
    public Id SalonId { get; set; }
    public virtual Salon? Salon { get; set; }
}

internal class GetImageByIdQueryHandler(IEfRepository<Image> _images, IMapper _mapper)
    : IQueryHandler<GetImageByIdQuery, GetImageByIdQueryResponse>
{
    public async Task<Result<GetImageByIdQueryResponse>> Handle(GetImageByIdQuery query, CancellationToken cancellationToken)
    {
        var image = await _images.GetByIdAsync(query.ImageId);

        if (image is null)
        {
            return Error.NotFound;
        }

        return _mapper.Map<GetImageByIdQueryResponse>(image);
    }
}