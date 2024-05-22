
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Services.Queries;

namespace SmartSalon.Presentation.Web.Features.Services.Responses;

public class GetBookingByIdResponse : IMapFrom<GetBookingByIdQueryResponse>
{
    public Id Id { get; set; }
    public bool Done { get; set; }
    public required string Note { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }

    public required string ServiceName { get; set; }
    public required string CustomerName { get; set; }
    public required string WorkerNickname { get; set; }
    public required string SalonName { get; set; }
    public required string SalonProfilePictureUrl { get; set; }

    public Id ServiceId { get; set; }
    public Id SalonId { get; set; }
    public Id WorkerId { get; set; }
    public Id CustomerId { get; set; }
}