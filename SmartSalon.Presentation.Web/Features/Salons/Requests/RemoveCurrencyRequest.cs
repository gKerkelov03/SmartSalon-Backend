using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Salons.Commands;
using SmartSalon.Presentation.Web.Attributes;

namespace SmartSalon.Presentation.Web.Salons.Requests;

public class RemoveCurrencyRequest : IMapTo<RemoveCurrencyCommand>
{
    [ComesFromRoute(IdRouteParameterName)]
    public Id CurrencyId { get; set; }
    public Id SalonId { get; set; }
}

