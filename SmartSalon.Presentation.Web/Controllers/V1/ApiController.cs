using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SmartSalon.Presentation.Web.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize]
public class ApiController : ControllerBase
{
    [HttpGet]
    public IActionResult Index()
    {
        return NotFound();
    }
}

