using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartSalon.Application.Domain.Salons;
using static SmartSalon.Application.ApplicationConstants.Validation.Image;

namespace SmartSalon.Data.Configurations;

public class SalonImageConfiguration : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
        => builder
            .Property(salonImage => salonImage.Url)
            .HasMaxLength(MaxUrlLength);
}