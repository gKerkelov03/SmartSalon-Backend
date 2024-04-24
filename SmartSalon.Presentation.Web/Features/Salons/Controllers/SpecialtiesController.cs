using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartSalon.Application.Features.Salons.Commands;
using SmartSalon.Application.Features.Salons.Queries;
using SmartSalon.Application.Features.Users.Commands;
using SmartSalon.Presentation.Web.Attributes;
using SmartSalon.Presentation.Web.Controllers;
using SmartSalon.Presentation.Web.Features.Salons.Requests;

namespace SmartSalon.Presentation.Web.Features.Salons.Controllers;

public class SpecialtiesController(ISender _mediator, IMapper _mapper) : V1ApiController
{
    [HttpPost]
    [SuccessResponse(Status201Created)]
    [Authorize(Policy = IsOwnerOfTheSalonOrIsAdminPolicy)]
    public async Task<IActionResult> AddSpecialty(AddSpecialtyRequest request)
    {
        var command = _mapper.Map<AddSpecialtyCommand>(request);
        var result = await _mediator.Send(command);
        return ProblemDetailsOr(result =>
            Created(string.Empty, result.Value.CreatedSpecialtyId),
            result
        );
    }

    [HttpGet(IdRoute)]
    [SuccessResponse(Status200OK)]
    [FailureResponse(Status404NotFound)]
    public async Task<IActionResult> GetSpecialtyById(Id specialtyId)
    {
        var command = new GetSpecialtyByIdQuery(specialtyId);
        var result = await _mediator.Send(command);

        return ProblemDetailsOr(result => Ok(result.Value), result);
    }

    [HttpPatch(IdRoute)]
    [SuccessResponse(Status200OK)]
    [FailureResponse(Status404NotFound)]
    [Authorize(Policy = IsOwnerOfTheSalonOrIsAdminPolicy)]
    public async Task<IActionResult> UpdateSpecialty(UpdateSpecialtyRequest request)
    {
        var command = _mapper.Map<RegisterCommand>(request);
        var result = await _mediator.Send(command);

        return ProblemDetailsOr(result =>
            CreatedAndLocatedAt("GetUserById", result.Value.RegisteredUserId),
            result
        );
    }

    [HttpDelete(IdRoute)]
    [SuccessResponse(Status204NoContent)]
    [FailureResponse(Status404NotFound)]
    public async Task<IActionResult> DeleteSpecialty(Id specialtyId)
    {
        var command = new DeleteSpecialtyCommand(specialtyId);
        var result = await _mediator.Send(command);

        return ProblemDetailsOr<NoContentResult>(result);
    }
}
