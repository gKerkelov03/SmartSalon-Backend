using Microsoft.AspNetCore.Identity;
using SmartSalon.Application.Domain.Abstractions;

namespace SmartSalon.Application.Domain;

public class User : IdentityUser<Id>, IDeletableEntity
{
    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public Id? PictureId { get; set; }

    public Image? Picture { get; set; }

    public bool IsDeleted { get; set; }

    public DateTimeOffset? DeletedOn { get; set; }

    public Id? DeletedBy { get; set; }
}
