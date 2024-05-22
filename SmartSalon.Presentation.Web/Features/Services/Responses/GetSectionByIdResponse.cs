using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Salons.Queries;

namespace SmartSalon.Presentation.Web.Features.Services.Responses;

public class GetSectionByIdResponse : IMapFrom<GetSalonByIdQueryResponse>
{
    public Id Id { get; set; }
    public Id SalonId { get; set; }
    public required string Name { get; set; }
    public required int Order { get; set; }
    public required string PictureUrl { get; set; }
}