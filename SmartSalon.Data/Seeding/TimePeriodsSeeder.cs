
namespace SmartSalon.Data.Seeding;

internal class TimePeriodsSeeder : ISeeder
{
    public async Task SeedAsync(SmartSalonDbContext dbContext, IServiceProvider serviceProvider)
    {
        await dbContext.TimePeriods.AddRangeAsync([
            new()
            {
                From = new(7),
                To = new(8)
            },
            new()
            {
                From = new(8),
                To = new(9)
            },
            new()
            {
                From = new(9),
                To = new(10)
            }
        ]);
    }
}

