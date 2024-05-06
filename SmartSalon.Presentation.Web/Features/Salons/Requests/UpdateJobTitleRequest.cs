using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Salons.Commands;

namespace SmartSalon.Presentation.Web.Features.Salons.Requests;

public class UpdateJobTitleRequest : IMapTo<UpdateSpecialtyCommand>
{
    public Id JobTitleId { get; set; }
    public required string Name { get; set; }
    public Id SalonId { get; set; }
}