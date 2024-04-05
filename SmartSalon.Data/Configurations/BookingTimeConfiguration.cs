using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartSalon.Application.Domain;

namespace SmartSalon.Data.Configurations;

public class TimePeriodConfiguration : IEntityTypeConfiguration<TimePeriod>
{
    public void Configure(EntityTypeBuilder<TimePeriod> builder)
        => builder.HasKey(bookingTime => new { bookingTime.From, bookingTime.To });
}