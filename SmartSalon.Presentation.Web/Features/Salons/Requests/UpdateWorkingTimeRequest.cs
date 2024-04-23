﻿using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Presentation.Web.Attributes;
using SmartWorkingTime.Application.Features.Salons.Commands;

namespace SmartSalon.Presentation.Web.Features.Salons.Requests;

public class UpdateWorkingTimeRequest : IMapTo<UpdateWorkingTimeCommand>
{
    [IdRouteParameter]
    public Id WorkingTimeId { get; set; }
    public required DayOfWeek DayOfWeek { get; set; }
    public TimeOnly From { get; set; }
    public TimeOnly To { get; set; }
}