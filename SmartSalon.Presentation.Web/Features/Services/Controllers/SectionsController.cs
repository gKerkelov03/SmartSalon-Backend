using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using SmartSalon.Presentation.Web.Controllers;
using SmartSalon.Presentation.Web.Attributes;
using SmartSalon.Presentation.Web.Features.Services.Requests;
using SmartSection.Application.Features.Services.Commands;
using SmartSalon.Application.Features.Services.Commands;
using SmartSalon.Presentation.Web.Features.Services.Responses;
using SmartSalon.Application.Features.Services.Queries;

namespace SmartSection.Presentation.Web.Features.Services.Controllers;

public class SectionsController(ISender _mediator, IMapper _mapper) : V1ApiController
{
    [HttpPost]
    [SuccessResponse(Status201Created)]
    [Authorize(Policy = IsAdminPolicy)]
    public async Task<IActionResult> CreateSection(CreateSectionRequest request)
    {
        var command = _mapper.Map<CreateSectionCommand>(request);

        var result = await _mediator.Send(command);

        return ProblemDetailsOr(
            result => CreatedAndLocatedAt(nameof(GetSectionById), result.Value.CreatedSectionId),
            result
        );
    }

    [HttpGet(IdRoute)]
    [SuccessResponse<GetSectionByIdResponse>(Status200OK)]
    [FailureResponse(Status404NotFound)]
    public async Task<IActionResult> GetSectionById(Id salonId)
    {
        var query = new GetSectionByIdQuery(salonId);
        var result = await _mediator.Send(query);

        return ProblemDetailsOr((result) =>
            Ok(_mapper.Map<GetSectionByIdResponse>(result.Value)),
            result
        );
    }

    [HttpPatch(IdRoute)]
    [SuccessResponse(Status200OK)]
    [FailureResponse(Status404NotFound)]
    [Authorize(Policy = IsOwnerOfTheSalonOrIsAdminPolicy)]
    public async Task<IActionResult> UpdateSection(UpdateSectionRequest request)
    {
        var command = _mapper.Map<UpdateSectionCommand>(request);
        var result = await _mediator.Send(command);

        return ProblemDetailsOr<OkResult>(result);
    }

    [HttpPatch(IdRoute)]
    [SuccessResponse(Status200OK)]
    [FailureResponse(Status404NotFound)]
    [Authorize(Policy = IsOwnerOfTheSalonOrIsAdminPolicy)]
    public async Task<IActionResult> MoveSection(MoveSectionRequest request)
    {
        var command = _mapper.Map<MoveSectionCommand>(request);
        var result = await _mediator.Send(command);

        return ProblemDetailsOr<OkResult>(result);
    }

    [HttpDelete(IdRoute)]
    [SuccessResponse(Status204NoContent)]
    [FailureResponse(Status404NotFound)]
    [Authorize(Policy = IsAdminPolicy)]
    public async Task<IActionResult> DeleteSection(Id sectionId)
    {
        var command = new DeleteSectionCommand(sectionId);
        var result = await _mediator.Send(command);

        return ProblemDetailsOr<NoContentResult>(result);
    }
}