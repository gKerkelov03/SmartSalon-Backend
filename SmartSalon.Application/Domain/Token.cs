
using SmartSalon.Application.Domain.Base;
using SmartSalon.Application.Domain.Enums;
using SmartSalon.Application.Domain.Users;

namespace SmartSalon.Application.Domain;

public class Token : DeletableEntity
{
    public Id UserId { get; set; }
    public virtual User? User { get; set; }
    public TokenType Type { get; set; }
    public required string Value { get; set; }
}
