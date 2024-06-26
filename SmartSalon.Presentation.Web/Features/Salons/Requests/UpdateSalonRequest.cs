﻿using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Salons.Commands;
using SmartSalon.Presentation.Web.Attributes;

namespace SmartSalon.Presentation.Web.Features.Salons.Requests;

public class UpdateSalonRequest : IMapTo<UpdateSalonCommand>
{
    [ComesFromRoute(IdRouteParameterName)]
    public Id SalonId { get; set; }
    public Id MainCurrencyId { get; set; }
    public required string Name { get; set; }

    public required string GoogleMapsLocation { get; set; }
    public required string Latitude { get; set; }
    public required string Longitude { get; set; }
    public required string Description { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public required int TimePenalty { get; set; }
    public required int BookingsInAdvance { get; set; }
    public bool SubscriptionsEnabled { get; set; }
    public bool WorkersCanMoveBookings { get; set; }
    public bool WorkersCanSetNonWorkingPeriods { get; set; }
}