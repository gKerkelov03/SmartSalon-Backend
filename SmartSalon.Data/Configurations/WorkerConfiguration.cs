using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartSalon.Application.Domain.Users;
using static SmartSalon.Application.ApplicationConstants.Validation.Worker;

namespace SmartSalon.Data.Configurations;

public class WorkerConfiguration : IEntityTypeConfiguration<Worker>
{
    public void Configure(EntityTypeBuilder<Worker> builder)
    {
        builder.Property(user => user.Nickname).HasMaxLength(NicknameLength);
        builder.Property(user => user.JobTitle).HasMaxLength(JobTitleLength);

        builder
            .HasDiscriminator<string>("UserType")
            .HasValue<Customer>(CustomerRoleName)
            .HasValue<Owner>(OwnerRoleName)
            .HasValue<Worker>(WorkerRoleName)
            .HasValue<User>(AdminRoleName);
    }
}