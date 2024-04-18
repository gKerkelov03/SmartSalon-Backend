using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartSalon.Application.Domain.Users;
using static SmartSalon.Application.ApplicationConstants.Validation.Role;

namespace SmartSalon.Data.Configurations.Users;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");

        builder
            .Property(role => role.Name)
            .HasMaxLength(MaxNameLength)
            .IsRequired();

        builder
            .Property(role => role.NormalizedName)
            .HasMaxLength(MaxNameLength)
            .IsRequired();
    }
}