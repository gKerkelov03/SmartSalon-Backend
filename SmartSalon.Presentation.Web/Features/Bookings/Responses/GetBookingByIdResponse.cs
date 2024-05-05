
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Services.Queries;

namespace SmartSalon.Presentation.Web.Features.Services.Responses;

public class GetBookingByIdResponse : IMapFrom<GetBookingByIdQueryResponse>
{
    public Id Id { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly From { get; set; }
    public TimeOnly To { get; set; }
    public Id ServiceId { get; set; }
    public Id CustomerId { get; set; }
    public Id SalonId { get; set; }
    public Id WorkerId { get; set; }
}