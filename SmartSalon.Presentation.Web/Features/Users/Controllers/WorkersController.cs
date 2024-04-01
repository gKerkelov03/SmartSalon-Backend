using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartSalon.Presentation.Web.Features.Users.Requests;
using SmartSalon.Application.Extensions;
using SmartSalon.Presentation.Web.Features.Users.Responses;
using SmartSalon.Presentation.Web.Features;
using SmartSalon.Presentation.Web.Features.Users.Controllers;
using SmartSalon.Application.Features.Users.Commands;
using SmartSalon.Presentation.Web.Attributes;

namespace SmartSalon.Presentation.Web.Users.Controllers;

public class WorkersController(ISender _sender) : ApiController
{
    [HttpPost]
    [SuccessResponse(Status201Created)]
    [FailureResponse(Status409Conflict)]
    public async Task<IActionResult> CreateWorker(CreateWorkerRequest request)
    {
        var command = request.MapTo<CreateWorkerCommand>();
        var result = await _sender.Send(command);

        return ProblemDetailsOr(result =>
            CreatedAndLocatedAt(nameof(UsersController), "GetUserById", result.Value.MapTo<CreateUserResponse>().CreatedUserId),
            result
        );
    }

    [HttpDelete(IdRouteParameter)]
    [SuccessResponse(Status204NoContent)]
    public async Task<IActionResult> DeleteWorker(Id workerId)
    {
        var command = new DeleteWorkerCommand { WorkerId = workerId };
        var result = await _sender.Send(command);

        return ProblemDetailsOr(NoContent, result);
    }
}
