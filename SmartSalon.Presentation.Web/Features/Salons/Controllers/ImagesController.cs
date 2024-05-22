using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartSalon.Application.Features.Salons.Commands;
using SmartSalon.Application.Features.Salons.Queries;
using SmartSalon.Presentation.Web.Attributes;
using SmartSalon.Presentation.Web.Controllers;
using SmartSalon.Presentation.Web.Features.Salons.Responses;
using SmartSalon.Presentation.Web.Salons.Requests;

namespace SmartSalon.Presentation.Web.Features.Salons.Controllers;

public class ImagesController(ISender _mediator, IMapper _mapper) : V1ApiController
{
    [HttpPost]
    [SuccessResponse(Status200OK)]
    [Authorize(Policy = IsOwnerOfTheSalonOrIsAdminPolicy)]
    public async Task<IActionResult> CreateImage(CreateImageRequest request)
    {
        var command = _mapper.Map<CreateImageCommand>(request);
        var result = await _mediator.Send(command);

        return ProblemDetailsOr(result =>
            CreatedAndLocatedAt(nameof(GetImageById), result.Value.CreatedImageId),
            result
        );
    }

    [HttpGet(IdRoute)]
    [SuccessResponse(Status200OK)]
    [FailureResponse(Status404NotFound)]
    public async Task<IActionResult> GetImageById(Id imageId)
    {
        var command = new GetImageByIdQuery(imageId);
        var result = await _mediator.Send(command);

        return ProblemDetailsOr(result =>
            Ok(_mapper.Map<GetImageByIdResponse>(result.Value)),
            result
        );
    }

    [HttpDelete(IdRoute)]
    [SuccessResponse(Status200OK)]
    [Authorize(Policy = IsOwnerOfTheSalonOrIsAdminPolicy)]
    public async Task<IActionResult> DeleteImage(DeleteImageRequest request)
    {
        var command = _mapper.Map<DeleteImageCommand>(request);
        var result = await _mediator.Send(command);

        return ProblemDetailsOr<NoContentResult>(result);
    }
}
