using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartSalon.Application.Domain;
using SmartSalon.Application.Domain.Salons;
using static SmartSalon.Application.ApplicationConstants.Validation.Salon;

namespace SmartSalon.Data.Configurations;

public class SalonConfiguration : IEntityTypeConfiguration<Salon>
{
    public void Configure(EntityTypeBuilder<Salon> builder)
    {
        builder
            .Property(salon => salon.Name)
            .HasMaxLength(MaxNameLength);

        builder
            .Property(salon => salon.Description)
            .HasMaxLength(MaxDescriptionLength);

        builder
            .Property(salon => salon.Location)
            .HasMaxLength(MaxLocationLength);

        builder
            .Property(salon => salon.DefaultTimePenalty)
            .HasMaxLength(MaxDefaultTimePenalty);

        builder
            .Property(salon => salon.DefaultBookingsInAdvance)
            .HasMaxLength(MaxDefaultBookingsInAdvance);

        builder
            .Property(salon => salon.WorkersCanMoveBookings)
            .HasDefaultValue(true);

        builder
            .Property(salon => salon.WorkersCanSetNonWorkingPeriods)
            .HasDefaultValue(true);

        builder
            .Property(salon => salon.SubscriptionsEnabled)
            .HasDefaultValue(true);

        builder
            .HasOne(salon => salon.WorkingTime)
            .WithOne(workingTime => workingTime.Salon)
            .HasForeignKey<WorkingTime>(workingTime => workingTime.SalonId);

        // builder
        // .HasMany(s => s.Images);
        // builder
        //     .HasOne(salon => salon.MainPicture)
        //     .WithOne()
        //     .HasForeignKey<Salon>(asdf => asdf.MainPictureId);
    }
}