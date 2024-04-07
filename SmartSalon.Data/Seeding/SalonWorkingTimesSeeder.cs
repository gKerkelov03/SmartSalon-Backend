
using Microsoft.EntityFrameworkCore;

namespace SmartSalon.Data.Seeding;

internal class WorkingTimesSeeder : ISeeder
{
    public async Task SeedAsync(SmartSalonDbContext dbContext, IServiceProvider serviceProvider)
    {
        var workingTimeId = Id.NewGuid();
        var salon = await dbContext.Salons.FirstAsync();
        salon.WorkingTimeId = workingTimeId;

        await dbContext.WorkingTimes.AddAsync(new()
        {
            Id = workingTimeId,
            SalonId = salon.Id,
            MondayFrom = new(9, 0),
            MondayTo = new(19, 0),
            TuesdayFrom = new(7, 0),
            TuesdayTo = new(19, 0),
            WednesdayFrom = new(7, 0),
            WednesdayTo = new(19, 0),
            ThursdayFrom = new(7, 0),
            ThursdayTo = new(19, 0),
            FridayFrom = new(7, 0),
            FridayTo = new(19, 0),
            SaturdayFrom = new(7, 0),
            SaturdayTo = new(19, 0),
            SundayFrom = new(7, 0),
            SundayTo = new(19, 0)
        });
    }
}

