using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartSalon.Application.Domain;
using static SmartSalon.Application.ApplicationConstants.Validation.Service;

namespace SmartSalon.Data.Configurations;

public class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder
            .Property(salon => salon.Name)
            .HasMaxLength(MaxNameLength);

        builder
            .Property(salon => salon.Description)
            .HasMaxLength(MaxDescriptionLength);
    }
}