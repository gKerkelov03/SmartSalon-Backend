using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartSalon.Application.Domain;
using static SmartSalon.Application.ApplicationConstants.Validation.Salon;

namespace SmartSalon.Data.Configurations;

public class SalonConfiguration : IEntityTypeConfiguration<Salon>
{
    public void Configure(EntityTypeBuilder<Salon> builder)
    {
        builder.Property(salon => salon.Name).HasMaxLength(NameLength);
        builder.Property(salon => salon.Description).HasMaxLength(DescriptionLength);
        builder.Property(salon => salon.Location).HasMaxLength(LocationLength);
        builder.Property(salon => salon.DefaultTimePenalty).HasMaxLength(MaximumDefaultTimePenalty);
        builder.Property(salon => salon.DefaultBookingsInAdvance).HasMaxLength(MaximumDefaultBookingsInAdvance);
        builder.Property(salon => salon.WorkersCanMoveBookings).HasDefaultValue(true);
        builder.Property(salon => salon.WorkersCanSetNonWorkingPeriods).HasDefaultValue(true);
        builder.Property(salon => salon.SubscriptionsEnabled).HasDefaultValue(true);
    }
}