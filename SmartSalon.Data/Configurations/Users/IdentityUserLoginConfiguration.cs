using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SmartSalon.Data.Configurations.Users;

public class IdentityUserLoginConfiguration : IEntityTypeConfiguration<IdentityUserLogin<Id>>
{
    public void Configure(EntityTypeBuilder<IdentityUserLogin<Id>> builder)
        => builder.ToTable("Logins");
}