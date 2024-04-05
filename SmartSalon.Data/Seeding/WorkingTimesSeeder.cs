
using Microsoft.EntityFrameworkCore;

namespace SmartSalon.Data.Seeding;

internal class WorkingTimesSeeder : ISeeder
{
    public async Task SeedAsync(SmartSalonDbContext dbContext, IServiceProvider serviceProvider)
    {
        var timePeriod = await dbContext.TimePeriods.FirstAsync();

        await dbContext.WorkingTimes.AddAsync(new()
        {
            MondayId = timePeriod.Id,
            TuesdayId = timePeriod.Id,
            WednesdayId = timePeriod.Id,
            ThursdayId = timePeriod.Id,
            FridayId = timePeriod.Id,
            SaturdayId = timePeriod.Id,
            SundayId = timePeriod.Id,
        });
    }
}

