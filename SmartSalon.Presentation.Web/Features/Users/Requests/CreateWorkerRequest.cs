using System.ComponentModel.DataAnnotations;
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Commands;

namespace SmartSalon.Presentation.Web.Features.Users.Requests;

public class CreateWorkerRequest : IMapTo<CreateWorkerCommand>
{
    [Required]
    public required Id SalonId { get; set; }

    [Required]
    public required string FirstName { get; set; }

    [Required]
    public required string LastName { get; set; }

    [Required]
    public required string Email { get; set; }

    [Required]
    public required string Password { get; set; }

    [Required]
    public required string PictureUrl { get; set; }
}