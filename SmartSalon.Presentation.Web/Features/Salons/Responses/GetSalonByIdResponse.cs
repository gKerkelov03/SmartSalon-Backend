using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Salons.Queries;

namespace SmartSalon.Presentation.Web.Features.Salons.Responses;

public class GetSalonByIdResponse : IMapFrom<GetSalonByIdQueryResponse>
{
    public Id Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Location { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public required int DefaultTimePenalty { get; set; }
    public required int DefaultBookingsInAdvance { get; set; }
    public bool SubscriptionsEnabled { get; set; }
    public bool SectionsEnabled { get; set; }
    public bool WorkersCanMoveBookings { get; set; }
    public bool WorkersCanSetNonWorkingPeriods { get; set; }
    public Id WorkingTimeId { get; set; }
    public IEnumerable<Id> Currencies { get; set; } = [];
    public IEnumerable<Id> Owners { get; set; } = [];
    public IEnumerable<Id> Workers { get; set; } = [];
    public IEnumerable<Id> Specialties { get; set; } = [];
    public IEnumerable<Id> Sections { get; set; } = [];
    public IEnumerable<Id> Categories { get; set; } = [];
    public IEnumerable<Id> Services { get; set; } = [];
    public IEnumerable<Id> Images { get; set; } = [];
}