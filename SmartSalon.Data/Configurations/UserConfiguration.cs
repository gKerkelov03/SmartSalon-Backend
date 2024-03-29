using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartSalon.Application.Domain;

namespace SmartSalon.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder
            .HasDiscriminator<string>("UserType")
            .HasValue<Customer>(CustomerRoleName)
            .HasValue<Owner>(OwnerRoleName)
            .HasValue<Worker>(WorkerRoleName);
    }
}