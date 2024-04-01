using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartSalon.Application.Domain;
using static SmartSalon.Application.ApplicationConstants.Validation.User;

namespace SmartSalon.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.Property(user => user.Email).HasMaxLength(EmailLength);
        builder.Property(user => user.NormalizedEmail).HasMaxLength(EmailLength);
        builder.Property(user => user.UserName).HasMaxLength(UserNameLength);
        builder.Property(user => user.NormalizedUserName).HasMaxLength(UserNameLength);
        builder.Property(user => user.FirstName).HasMaxLength(FirstNameLength);
        builder.Property(user => user.LastName).HasMaxLength(LastNameLength);

        builder
            .HasDiscriminator<string>("UserType")
            .HasValue<Customer>(CustomerRoleName)
            .HasValue<Owner>(OwnerRoleName)
            .HasValue<Worker>(WorkerRoleName);
    }
}