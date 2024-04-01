
using SmartSalon.Application.Domain.Abstractions;
using SmartSalon.Application.Domain.Enums;

namespace SmartSalon.Application.Domain;

public class Token : IBaseEntity
{
    public Id Id { get; set; } = Id.NewGuid();

    public Id UserId { get; set; } = Id.NewGuid();

    public virtual User? User { get; set; }

    public TokenType Type { get; set; }

    public required string Value { get; set; }
}
