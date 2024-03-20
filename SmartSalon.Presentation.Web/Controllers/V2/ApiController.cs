using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SmartSalon.Presentation.Web.Controllers.V2;

[ApiController]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize]
public class ApiController : ControllerBase
{
    [HttpGet]
    public IActionResult Index()
    {
        return NotFound("The V2 api is not implemented yet");
    }
}

