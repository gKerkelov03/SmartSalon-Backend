using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Salons.Queries;

namespace SmartSalon.Presentation.Web.Features.Salons.Responses;

public class GetCurrencyByIdResponse : IMapFrom<GetCurrencyByIdQueryResponse>
{
    public Id Id { get; set; }
    public required string Code { get; set; }
    public required string Name { get; set; }
    public required string Country { get; set; }
}