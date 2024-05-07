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
    public required int TimePenalty { get; set; }
    public required int BookingsInAdvance { get; set; }
    public bool SubscriptionsEnabled { get; set; }
    public bool SectionsEnabled { get; set; }
    public bool WorkersCanMoveBookings { get; set; }
    public bool WorkersCanSetNonWorkingPeriods { get; set; }
    public Id WorkingTimeId { get; set; }
    public Id MainCurrencyId { get; set; }
    public required IEnumerable<Id> AcceptedCurrencies { get; set; }
    public required IEnumerable<Id> Owners { get; set; }
    public required IEnumerable<Id> Workers { get; set; }
    public required IEnumerable<Id> Specialties { get; set; }
    public required IEnumerable<Id> Sections { get; set; }
    public required IEnumerable<Id> Images { get; set; }
    public required IEnumerable<Id> JobTitles { get; set; }
}