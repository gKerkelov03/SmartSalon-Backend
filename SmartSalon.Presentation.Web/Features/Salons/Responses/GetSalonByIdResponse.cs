using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Salons.Queries;
using SmartSalon.Data.Configurations;

namespace SmartSalon.Presentation.Web.Features.Salons.Responses;

public class GetSalonByIdResponse : IMapFrom<GetSalonByIdQueryResponse>
{
    public Id Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string GoogleMapsLocation { get; set; }
    public required string Latitude { get; set; }
    public required string Longitude { get; set; }
    public required string Country { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public required int TimePenalty { get; set; }
    public required int BookingsInAdvance { get; set; }
    public bool SubscriptionsEnabled { get; set; }
    public bool WorkersCanMoveBookings { get; set; }

    public bool WorkersCanDeleteBookings { get; set; }
    public bool WorkersCanSetNonWorkingPeriods { get; set; }
    public Id WorkingTimeId { get; set; }

    public required GetCurrencyByIdResponse MainCurrency { get; set; }
    public required IEnumerable<GetCurrencyByIdResponse> OtherAcceptedCurrencies { get; set; }
    public required IEnumerable<GetSpecialtyByIdResponse> Specialties { get; set; }
    public required IEnumerable<GetImageByIdResponse> Images { get; set; }
    public required IEnumerable<GetJobTitleByIdResponse> JobTitles { get; set; }

    public required IEnumerable<Id> Owners { get; set; }
    public required IEnumerable<Id> Workers { get; set; }
    public required IEnumerable<Id> Sections { get; set; }
}