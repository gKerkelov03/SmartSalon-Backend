using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartSalon.Application.Domain.Salons;
using SmartSalon.Data.SeedingData;
using static SmartSalon.Application.ApplicationConstants.Validation.Salon;

namespace SmartSalon.Data.Configurations;

public class SalonConfiguration : IEntityTypeConfiguration<Salon>
{
    public void Configure(EntityTypeBuilder<Salon> builder)
    {
        builder
            .HasOne(salon => salon.WorkingTime)
            .WithOne(workingTime => workingTime.Salon)
            .HasForeignKey<WorkingTime>(workingTime => workingTime.SalonId);

        builder
            .HasOne(salon => salon.MainCurrency)
            .WithMany()
            .HasForeignKey(bs => bs.MainCurrencyId);

        builder
            .HasMany(salon => salon.AcceptedCurrencies)
            .WithMany(currency => currency.Salons);

        builder.HasData(SalonsSeedingData.Data);

        builder
            .Property(salon => salon.Name)
            .HasMaxLength(MaxNameLength);

        builder
            .Property(salon => salon.Description)
            .HasMaxLength(MaxDescriptionLength);

        builder
            .Property(salon => salon.GoogleMapsLocation)
            .HasMaxLength(MaxGoogleMapsLocationLength);

        builder
            .Property(salon => salon.TimePenalty)
            .HasMaxLength(MaxTimePenalty);

        builder
            .Property(salon => salon.BookingsInAdvance)
            .HasMaxLength(MaxBookingsInAdvance);

        builder
            .Property(salon => salon.WorkersCanMoveBookings)
            .HasDefaultValue(true);

        builder
            .Property(salon => salon.WorkersCanSetNonWorkingPeriods)
            .HasDefaultValue(true);

        builder
            .Property(salon => salon.SubscriptionsEnabled)
            .HasDefaultValue(true);
    }
}