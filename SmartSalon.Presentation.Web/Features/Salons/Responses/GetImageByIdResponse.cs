
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Salons.Queries;

namespace SmartSalon.Presentation.Web.Features.Salons.Responses;

public class GetImageByIdResponse : IMapFrom<GetImageByIdQueryResponse>
{
    public Id Id { get; set; }
    public required string Url { get; set; }
    public Id SalonId { get; set; }
}