
using SmartSalon.Application.Commands.Responses;
using SmartSalon.Application.Mapping;

namespace SmartSalon.Presentation.Web.Models.Responses;

public class LoginResponse : IMapFrom<LoginCommandResponse>
{
    public required string Jwt { get; set; }
}

