using System.ComponentModel.DataAnnotations;
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Commands;

namespace SmartSalon.Presentation.Web.Features.Users.Requests;

public class LoginRequest : IMapTo<LoginCommand>
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}