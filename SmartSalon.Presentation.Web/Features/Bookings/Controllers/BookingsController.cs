using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartBooking.Application.Features.Bookings.Commands;
using SmartSalon.Application.Features.Bookings.Commands;
using SmartSalon.Application.Features.Services.Commands;
using SmartSalon.Application.Features.Services.Queries;
using SmartSalon.Presentation.Web.Attributes;
using SmartSalon.Presentation.Web.Controllers;
using SmartSalon.Presentation.Web.Features.Bookings.Requests;
using SmartSalon.Presentation.Web.Features.Services.Requests;
using SmartSalon.Presentation.Web.Features.Services.Responses;

namespace SmartSalon.Presentation.Web.Features.Services.Controllers;

public class BookingsController(ISender _mediator, IMapper _mapper) : V1ApiController
{
    [HttpPost]
    [SuccessResponse(Status200OK)]
    public async Task<IActionResult> CreateBooking(CreateBookingRequest request)
    {
        var command = _mapper.Map<CreateBookingCommand>(request);
        var result = await _mediator.Send(command);

        return ProblemDetailsOr(result =>
            CreatedAndLocatedAt(nameof(GetBookingById), result.Value.CreatedBookingId),
            result
        );
    }

    [HttpGet(IdRoute)]
    [SuccessResponse(Status200OK)]
    [FailureResponse(Status404NotFound)]
    public async Task<IActionResult> GetBookingById(Id categoryId)
    {
        var command = new GetBookingByIdQuery(categoryId);
        var result = await _mediator.Send(command);

        return ProblemDetailsOr(result =>
            Ok(_mapper.Map<GetBookingByIdResponse>(result.Value)),
            result
        );
    }

    [HttpPatch(IdRoute)]
    [SuccessResponse(Status200OK)]
    [FailureResponse(Status404NotFound)]
    [Authorize(Policy = IsOwnerOfTheSalonOrIsAdminPolicy)]
    public async Task<IActionResult> UpdateBooking(UpdateBookingRequest request)
    {
        var command = _mapper.Map<UpdateBookingCommand>(request);
        var result = await _mediator.Send(command);

        return ProblemDetailsOr<OkResult>(result);
    }

    [HttpDelete]
    [SuccessResponse(Status200OK)]
    [Authorize(Policy = IsOwnerOfTheSalonOrIsAdminPolicy)]
    public async Task<IActionResult> DeleteBooking(DeleteBookingRequest request)
    {
        var command = _mapper.Map<DeleteBookingCommand>(request);
        var result = await _mediator.Send(command);

        return ProblemDetailsOr<NoContentResult>(result);
    }
}
