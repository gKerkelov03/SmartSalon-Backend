using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartSalon.Data.SeedingData;

namespace SmartSalon.Data.Configurations.Users;

public class IdentityUserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<Id>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<Id>> builder)
    {
        builder.ToTable("UserRole");
        builder.HasData(UserRolesSeedingData.Data);
    }
}