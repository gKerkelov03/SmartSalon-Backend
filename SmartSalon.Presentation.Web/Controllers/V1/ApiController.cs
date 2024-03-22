using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SmartSalon.Presentation.Web.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ProducesResponseType(typeof(UnauthorizedResponse), StatusCodes.Status401Unauthorized)]
// [Authorize]
public abstract class ApiController : ControllerBase
{
    protected readonly ISender _sender;

    public ApiController(ISender sender)
        => _sender = sender;
}

