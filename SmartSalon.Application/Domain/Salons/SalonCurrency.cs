using SmartSalon.Application.Domain.Base;

namespace SmartSalon.Application.Domain;

public class SalonCurrency : BaseEntity
{
    public required string Code { get; set; }
    public required string Name { get; set; }
    public required string Country { get; set; }
}