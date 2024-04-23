
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Salons.Commands;

namespace SmartSalon.Presentation.Web.Salons.Requests;

public class AddImageRequest : IMapTo<AddImageCommand>
{
    public required string Url { get; set; }
    public Id Salonid { get; set; }
}

