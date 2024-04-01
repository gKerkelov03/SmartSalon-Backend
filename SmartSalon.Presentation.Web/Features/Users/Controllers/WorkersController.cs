using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartSalon.Presentation.Web.Features.Users.Requests;
using SmartSalon.Application.Extensions;
using SmartSalon.Presentation.Web.Features.Users.Responses;
using SmartSalon.Presentation.Web.Features;
using SmartSalon.Presentation.Web.Features.Users.Controllers;
using SmartSalon.Application.Features.Users.Commands;

namespace SmartSalon.Presentation.Web.Users.Controllers;

public class WorkersController(ISender _sender) : ApiController
{
    [HttpPost]
    [ProducesResponseType(Status201Created)]
    [ProducesResponseType(typeof(ProblemDetailsWithErrors), Status409Conflict)]
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
    [ProducesResponseType(Status204NoContent)]
    public async Task<IActionResult> DeleteWorker(Id workerId)
    {
        var command = new DeleteWorkerCommand { WorkerId = workerId };
        var result = await _sender.Send(command);

        return ProblemDetailsOr(NoContent, result);
    }
}
