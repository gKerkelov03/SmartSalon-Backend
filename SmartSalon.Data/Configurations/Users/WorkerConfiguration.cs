using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartSalon.Application.Domain.Users;
using static SmartSalon.Application.ApplicationConstants.Validation.Worker;

namespace SmartSalon.Data.Configurations.Users;

public class WorkerConfiguration : IEntityTypeConfiguration<Worker>
{
    public void Configure(EntityTypeBuilder<Worker> builder)
    {
        builder
            .Property(user => user.Nickname)
            .HasMaxLength(MaxNicknameLength);

        builder
            .Property(user => user.JobTitle)
            .HasMaxLength(MaxJobTitleLength);
    }
}