using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartSalon.Application.Domain;
using static SmartSalon.Application.ApplicationConstants.Validation.Currency;

namespace SmartSalon.Data.Configurations;

public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
{
    public void Configure(EntityTypeBuilder<Currency> builder)
    {
        builder
            .Property(currency => currency.Code)
            .HasMaxLength(MaxCodeLength);

        builder
            .Property(currency => currency.Name)
            .HasMaxLength(MaxNameLength);

        builder
            .Property(currency => currency.Country)
            .HasMaxLength(MaxCountryLength);
    }
}