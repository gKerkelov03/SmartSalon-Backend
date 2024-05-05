using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartSalon.Application.Features.Salons.Commands;
using SmartSalon.Application.Features.Salons.Queries;
using SmartSalon.Presentation.Web.Attributes;
using SmartSalon.Presentation.Web.Controllers;
using SmartSalon.Presentation.Web.Features.Salons.Requests;
using SmartSalon.Presentation.Web.Features.Salons.Responses;
using SmartSalon.Presentation.Web.Salons.Requests;

namespace SmartSalon.Presentation.Web.Features.Salons.Controllers;

public class JobTitlesController(ISender _mediator, IMapper _mapper) : V1ApiController
{
    [HttpPost]
    [SuccessResponse(Status201Created)]
    [Authorize(Policy = IsOwnerOfTheSalonOrIsAdminPolicy)]
    public async Task<IActionResult> CreateJobTitle(CreateJobTitleRequest request)
    {
        var command = _mapper.Map<CreateJobTitleCommand>(request);
        var result = await _mediator.Send(command);

        return ProblemDetailsOr(result =>
            CreatedAndLocatedAt(nameof(GetJobTitleById), result.Value.CreatedJobTitleId),
            result
        );
    }

    [HttpGet(IdRoute)]
    [SuccessResponse(Status200OK)]
    [FailureResponse(Status404NotFound)]
    public async Task<IActionResult> GetJobTitleById(Id specialtyId)
    {
        var command = new GetJobTitleByIdQuery(specialtyId);
        var result = await _mediator.Send(command);

        return ProblemDetailsOr(result =>
            Ok(_mapper.Map<GetJobTitleByIdResponse>(result.Value)),
            result
        );
    }

    [HttpPatch]
    [SuccessResponse(Status200OK)]
    [FailureResponse(Status404NotFound)]
    [Authorize(Policy = IsOwnerOfTheSalonOrIsAdminPolicy)]
    public async Task<IActionResult> UpdateJobTitle(UpdateJobTitleRequest request)
    {
        var command = _mapper.Map<UpdateJobTitleCommand>(request);
        var result = await _mediator.Send(command);

        return ProblemDetailsOr<OkResult>(result);
    }

    [HttpPatch(IdRoute)]
    [SuccessResponse(Status200OK)]
    [FailureResponse(Status404NotFound)]
    [Authorize(Policy = IsOwnerOfTheSalonOrIsAdminPolicy)]
    public async Task<IActionResult> UpdateJobTitlesOfWorker(UpdateJobTitlesOfWorkerRequest request)
    {
        var command = _mapper.Map<UpdateJobTitlesOfWorkerCommand>(request);
        var result = await _mediator.Send(command);

        return ProblemDetailsOr<OkResult>(result);
    }

    [HttpDelete(IdRoute)]
    [SuccessResponse(Status204NoContent)]
    [FailureResponse(Status404NotFound)]
    public async Task<IActionResult> DeleteJobTitle(DeleteJobTitleRequest request)
    {
        var command = _mapper.Map<DeleteJobTitleCommand>(request);
        var result = await _mediator.Send(command);

        return ProblemDetailsOr<NoContentResult>(result);
    }
}

