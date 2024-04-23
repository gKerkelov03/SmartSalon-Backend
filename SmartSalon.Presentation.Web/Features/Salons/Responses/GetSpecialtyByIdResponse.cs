using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Salons.Queries;

namespace SmartSalon.Presentation.Web.Features.Salons.Responses;

public class GetSpecialtyByIdResponse : IMapFrom<GetSpecialtyByIdQueryResponse>
{
    public required string Text { get; set; }
    public Id SalonId { get; set; }
}