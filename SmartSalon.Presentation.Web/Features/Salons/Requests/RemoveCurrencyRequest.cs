using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Salons.Commands;

namespace SmartSalon.Presentation.Web.Salons.Requests;

public class RemoveCurrencyRequest : IMapTo<RemoveCurrencyCommand>
{
    public Id CurrencyId { get; set; }
    public Id SalonId { get; set; }
}

