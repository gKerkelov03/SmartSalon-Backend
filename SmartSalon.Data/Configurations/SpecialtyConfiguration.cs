using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartSalon.Application.Domain.Salons;
using static SmartSalon.Application.ApplicationConstants.Validation.Specialty;

namespace SmartSalon.Data.Configurations;

public class SpecialtyConfiguration : IEntityTypeConfiguration<Specialty>
{
    public void Configure(EntityTypeBuilder<Specialty> builder)
    {
        builder
            .Property(specialty => specialty.Text)
            .HasMaxLength(MaxTextLength);
    }
}