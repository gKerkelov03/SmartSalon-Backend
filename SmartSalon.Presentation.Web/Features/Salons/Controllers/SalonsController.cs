using MediatR;
using SmartSalon.Application.Extensions;
using Microsoft.AspNetCore.Mvc;
using SmartSalon.Presentation.Web.Features.Salons.Requests;
using SmartSalon.Application.Features.Salons.Commands;
using SmartSalon.Presentation.Web.Attributes;
using SmartSalon.Application.Features.Salons.Queries;
using SmartSalon.Presentation.Web.Features.Salons.Responses;
using AutoMapper;
using SmartSalon.Presentation.Web.Controllers;
using SmartSalon.Presentation.Web.Salons.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace SmartSalon.Presentation.Web.Features.Salons.Controllers;

public class SalonsController(ISender _mediator, IMapper _mapper) : V1ApiController
{
    [HttpPost]
    [SuccessResponse(Status201Created)]
    [Authorize(Policy = IsAdminPolicy)]
    public async Task<IActionResult> CreateSalon(CreateSalonRequest request)
    {
        var command = _mapper.Map<CreateSalonCommand>(request);

        var result = await _mediator.Send(command);

        return ProblemDetailsOr(
            result => CreatedAndLocatedAt(nameof(GetSalonById), result.Value.CreatedSalonId),
            result
        );
    }

    [HttpGet(IdRoute)]
    [SuccessResponse<GetSalonByIdResponse>(Status200OK)]
    [FailureResponse(Status404NotFound)]
    public async Task<IActionResult> GetSalonById(Id salonId)
    {
        var query = new GetSalonByIdQuery(salonId);
        var result = await _mediator.Send(query);

        return ProblemDetailsOr((result) =>
            Ok(_mapper.Map<GetSalonByIdResponse>(result.Value)),
            result
        );
    }

    [HttpGet]
    [SuccessResponse<GetSalonByIdResponse>(Status200OK)]
    [FailureResponse(Status404NotFound)]
    public async Task<IActionResult> GetAllSalonsInCountry(string country)
    {
        var query = new GetAllSalonsInCountryQuery(country);
        var result = await _mediator.Send(query);

        return ProblemDetailsOr((result) =>
            Ok(result.Value.ToListOf<GetSalonByIdResponse>(_mapper)),
            result
        );
    }

    [HttpPatch(IdRoute)]
    [SuccessResponse(Status200OK)]
    [FailureResponse(Status404NotFound)]
    [Authorize(Policy = IsOwnerOfTheSalonOrIsAdminPolicy)]
    public async Task<IActionResult> UpdateSalon(UpdateSalonRequest request)
    {
        var command = _mapper.Map<UpdateSalonCommand>(request);
        var result = await _mediator.Send(command);

        return ProblemDetailsOr<OkResult>(result);
    }

    [HttpDelete(IdRoute)]
    [SuccessResponse(Status204NoContent)]
    [FailureResponse(Status404NotFound)]
    [Authorize(Policy = IsAdminPolicy)]
    public async Task<IActionResult> DeleteSalon(Id salonId)
    {
        var command = new DeleteSalonCommand(salonId);
        var result = await _mediator.Send(command);

        return ProblemDetailsOr<NoContentResult>(result);
    }

    [HttpPost($"InviteWorker")]
    [SuccessResponse(Status200OK)]
    [Authorize(Policy = IsOwnerOrIsAdminPolicy)]
    public async Task<IActionResult> InviteWorker(InviteWorkerRequest request)
    {
        var command = _mapper.Map<InviteWorkerCommand>(request);
        var result = await _mediator.Send(command);

        return ProblemDetailsOr<OkResult>(result);
    }

    [HttpPost($"InviteOwner")]
    [SuccessResponse(Status200OK)]
    [Authorize(Policy = IsOwnerOfTheSalonOrIsAdminPolicy)]
    public async Task<IActionResult> InviteOwner(InviteOwnerRequest request)
    {

        var command = _mapper.Map<InviteOwnerCommand>(request);
        var result = await _mediator.Send(command);

        return ProblemDetailsOr<OkResult>(result);
    }
}