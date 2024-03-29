using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartSalon.Application.Domain;

namespace SmartSalon.Data.Configurations;

public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
{
    public void Configure(EntityTypeBuilder<Profile> builder)
    {
        builder.ToTable("Profiles");

        builder
            .HasDiscriminator<string>("ProfileType")
            .HasValue<Customer>("Customer")
            .HasValue<Owner>("Owner")
            .HasValue<Worker>("Worker");
    }
}