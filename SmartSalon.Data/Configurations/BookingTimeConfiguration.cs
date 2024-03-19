using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartSalon.Application.Domain;

public class BookingTimeConfiguration : IEntityTypeConfiguration<BookingTime>
{
    public void Configure(EntityTypeBuilder<BookingTime> builder)
        => builder.HasKey(bookingTime => new { bookingTime.From, bookingTime.To });
}