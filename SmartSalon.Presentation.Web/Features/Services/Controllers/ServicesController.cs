using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartSalon.Presentation.Web.Attributes;
using SmartSalon.Presentation.Web.Controllers;
using SmartSalon.Presentation.Web.Features.Services.Requests;
using SmartSalon.Application.Features.Services.Queries;
using SmartSalon.Application.Features.Services.Commands;
using SmartService.Application.Features.Services.Commands;

namespace SmartSalon.Presentation.Web.Features.Services.Controllers;

public class ServicesController(ISender _mediator, IMapper _mapper) : V1ApiController
{
    [HttpPost]
    [SuccessResponse(Status200OK)]
    [FailureResponse(Status409Conflict)]
    [Authorize(Policy = IsOwnerOfTheSalonOrIsAdminPolicy)]
    public async Task<IActionResult> CreateService(CreateServiceRequest request)
    {
        var command = _mapper.Map<CreateServiceCommand>(request);
        var result = await _mediator.Send(command);

        return ProblemDetailsOr(result => Created(result.Value.CreatedServiceId), result);
    }

    [HttpPatch(IdRoute)]
    [SuccessResponse(Status200OK)]
    [FailureResponse(Status404NotFound)]
    [Authorize(Policy = IsOwnerOfTheSalonOrIsAdminPolicy)]
    public async Task<IActionResult> UpdateService(UpdateServiceRequest request)
    {
        var command = _mapper.Map<UpdateServiceCommand>(request);
        var result = await _mediator.Send(command);

        return ProblemDetailsOr<OkResult>(result);
    }

    [HttpDelete]
    [SuccessResponse(Status200OK)]
    [Authorize(Policy = IsOwnerOfTheSalonOrIsAdminPolicy)]
    public async Task<IActionResult> DeleteService(DeleteServiceRequest request)
    {
        var command = _mapper.Map<DeleteServiceCommand>(request);
        var result = await _mediator.Send(command);

        return ProblemDetailsOr<NoContentResult>(result);
    }
}
