using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SmartSalon.Data.Configurations;

public class IdentityUserTokenConfiguration : IEntityTypeConfiguration<IdentityUserToken<Id>>
{
    public void Configure(EntityTypeBuilder<IdentityUserToken<Id>> builder)
        => builder.ToTable("Tokens");
}