using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartSalon.Application.Domain.Salons;
using static SmartSalon.Application.ApplicationConstants.Validation.SalonSpecialty;

namespace SmartSalon.Data.Configurations;

public class SpecialtyConfiguration : IEntityTypeConfiguration<SalonSpecialty>
{
    public void Configure(EntityTypeBuilder<SalonSpecialty> builder)
    {
        builder
            .Property(specialty => specialty.Text)
            .HasMaxLength(MaxTextLength);
    }
}