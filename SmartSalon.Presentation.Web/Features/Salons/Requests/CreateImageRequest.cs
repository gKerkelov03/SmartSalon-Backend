
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Salons.Commands;

namespace SmartSalon.Presentation.Web.Salons.Requests;

public class CreateImageRequest : IMapTo<CreateImageCommand>
{
    public required string Url { get; set; }
    public Id SalonId { get; set; }
}

