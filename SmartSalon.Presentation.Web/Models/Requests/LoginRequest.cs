using System.ComponentModel.DataAnnotations;
using SmartSalon.Application.Commands;
using SmartSalon.Application.Mapping;

namespace SmartSalon.Presentation.Web.Models.Requests;

public class LoginRequest : IMapTo<LoginCommand>
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}