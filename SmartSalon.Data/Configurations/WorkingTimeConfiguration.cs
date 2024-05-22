using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartSalon.Application.Domain.Salons;
using SmartSalon.Data.SeedingData;

namespace SmartSalon.Data.Configurations;

public class WorkingTimeConfiguration : IEntityTypeConfiguration<WorkingTime>
{
    public void Configure(EntityTypeBuilder<WorkingTime> builder) => builder.HasData(WorkingTimesSeedingData.Data);
}