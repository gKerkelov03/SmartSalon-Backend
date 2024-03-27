using SmartSalon.Application.Domain.Abstractions;

namespace SmartSalon.Application.Domain;

public class Image : BaseEntity
{
    public required string Url { get; set; }
}