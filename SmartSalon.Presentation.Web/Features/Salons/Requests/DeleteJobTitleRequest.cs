using Microsoft.AspNetCore.Mvc;
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Salons.Commands;
using SmartSalon.Presentation.Web.Attributes;

namespace SmartSalon.Presentation.Web.Salons.Requests;

public class DeleteJobTitleRequest : IMapTo<DeleteJobTitleCommand>
{
    [ComesFromRoute(IdRouteParameterName)]
    public Id JobTitleId { get; set; }
    public Id SalonId { get; set; }
}

