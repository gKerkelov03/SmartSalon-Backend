﻿using Microsoft.AspNetCore.Mvc;
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Salons.Commands;

namespace SmartSalon.Presentation.Web.Features.Salons.Requests;

public class UpdateSpecialtyRequest : IMapTo<UpdateSpecialtyCommand>
{
    [FromRoute(Name = IdRouteParameterName)]
    public Id SpecialtyId { get; set; }
    public required string Text { get; set; }
    public Id SalonId { get; set; }
}