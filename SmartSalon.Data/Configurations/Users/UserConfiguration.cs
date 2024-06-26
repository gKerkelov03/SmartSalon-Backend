global using PasswordHasher = Microsoft.AspNetCore.Identity.PasswordHasher<SmartSalon.Application.Domain.Users.User>;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartSalon.Application.Domain.Users;
using SmartSalon.Application.Extensions;
using SmartSalon.Data.SeedingData;
using static SmartSalon.Application.ApplicationConstants.Validation.User;

namespace SmartSalon.Data.Configurations.Users;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasData(UsersSeedingData.AdminsData);

        builder.ToTable("Users");

        builder
            .HasDiscriminator<string>("UserType")
            .HasValue<Customer>(CustomerRoleName)
            .HasValue<Owner>(OwnerRoleName)
            .HasValue<Worker>(WorkerRoleName)
            .HasValue<User>(AdminRoleName);

        builder
            .Property(user => user.Email)
            .HasMaxLength(MaxEmailLength)
            .IsRequired();

        builder
            .Property(user => user.NormalizedEmail)
            .HasMaxLength(MaxEmailLength)
            .IsRequired();

        builder
            .Property(user => user.UserName)
            .HasMaxLength(MaxUserNameLength)
            .IsRequired();

        builder
            .Property(user => user.NormalizedUserName)
            .HasMaxLength(MaxUserNameLength)
            .IsRequired();

        builder
            .Property(user => user.FirstName)
            .HasMaxLength(MaxFirstNameLength);

        builder
            .Property(user => user.LastName)
            .HasMaxLength(MaxLastNameLength);

        builder
            .Property(user => user.PhoneNumber)
            .HasMaxLength(MaxPhoneNumberLength)
            .IsRequired();

        builder
            .Property(user => user.SecurityStamp)
            .IsRequired();

        builder
            .Property(user => user.ConcurrencyStamp)
            .IsRequired();
    }
}