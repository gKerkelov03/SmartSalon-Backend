using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartSalon.Application.Features.Salons.Queries;
using SmartSalon.Presentation.Web.Attributes;
using SmartSalon.Presentation.Web.Controllers;
using SmartSalon.Presentation.Web.Features.Salons.Requests;
using SmartWorkingTime.Application.Features.Salons.Commands;

namespace SmartSalon.Presentation.Web.Features.Salons.Controllers;

[Authorize(Policy = IsOwnerOfTheSalonOrIsAdminPolicy)]
public class WorkingTimesController(ISender _mediator, IMapper _mapper) : V1ApiController
{
    [HttpGet(IdRoute)]
    [SuccessResponse(Status200OK)]
    [FailureResponse(Status404NotFound)]
    public async Task<IActionResult> GetWorkingTimeById(Id workingTimeId)
    {
        var command = new GetWorkingTimeByIdQuery(workingTimeId);
        var result = await _mediator.Send(command);

        return ProblemDetailsOr(result => Ok(result.Value), result);
    }

    [HttpPatch]
    [SuccessResponse(Status200OK)]
    [FailureResponse(Status404NotFound)]
    public async Task<IActionResult> UpdateWorkingTime(UpdateWorkingTimeRequest request)
    {
        var command = _mapper.Map<UpdateWorkingTimeCommand>(request);
        var result = await _mediator.Send(command);

        return ProblemDetailsOr<OkResult>(result);
    }
}
