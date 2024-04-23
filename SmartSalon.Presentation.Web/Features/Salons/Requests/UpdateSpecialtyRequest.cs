using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Users.Commands;
using SmartSalon.Presentation.Web.Attributes;

namespace SmartSalon.Presentation.Web.Features.Salons.Requests;

public class UpdateSpecialtyRequest : IMapTo<UpdateUserCommand>
{
    [IdRouteParameter]
    public Id SpecialtyId { get; set; }
    public required string Text { get; set; }
    public Id SalonId { get; set; }
}