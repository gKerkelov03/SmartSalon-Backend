using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SmartSalon.Data.Configurations;

public class IdentityUserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<Id>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<Id>> builder)
        => builder.ToTable("ProfileRole");
}