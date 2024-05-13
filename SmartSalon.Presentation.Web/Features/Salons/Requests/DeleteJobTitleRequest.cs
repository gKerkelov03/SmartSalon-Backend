using Microsoft.AspNetCore.Mvc;
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Salons.Commands;

namespace SmartSalon.Presentation.Web.Salons.Requests;

public class DeleteJobTitleRequest : IMapTo<DeleteJobTitleCommand>
{
    [FromRoute(Name = IdRouteParameterName)]
    public Id JobTitleId { get; set; }
    public Id SalonId { get; set; }
}

