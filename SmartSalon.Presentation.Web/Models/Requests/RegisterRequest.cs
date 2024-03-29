using System.ComponentModel.DataAnnotations;
using SmartSalon.Application.Commands;
using SmartSalon.Application.Mapping;

namespace SmartSalon.Presentation.Web.Models.Requests;

public class RegisterRequest : IMapTo<RegisterCommand>
{
    [Required]
    public required string FirstName { get; set; }

    [Required]
    public required string LastName { get; set; }

    [Required]
    public required string Email { get; set; }

    [Required]
    public required string Password { get; set; }
}