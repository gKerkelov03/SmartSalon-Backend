using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartSalon.Application.Domain;
using static SmartSalon.Application.ApplicationConstants.Validation.SalonCurrency;

namespace SmartSalon.Data.Configurations;

public class CurrencyConfiguration : IEntityTypeConfiguration<SalonCurrency>
{
    public void Configure(EntityTypeBuilder<SalonCurrency> builder)
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