using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Salons.Commands;
using SmartSalon.Presentation.Web.Attributes;

namespace SmartSalon.Presentation.Web.Features.Salons.Requests;

public class UpdateJobTitleRequest : IMapTo<UpdateSpecialtyCommand>
{
    [IdRouteParameter]
    public Id JobTitleId { get; set; }
    public required string Name { get; set; }
    public Id SalonId { get; set; }
}