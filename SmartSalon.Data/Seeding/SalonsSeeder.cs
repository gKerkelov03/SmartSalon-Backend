
using Microsoft.EntityFrameworkCore;

namespace SmartSalon.Data.Seeding;

internal class SalonsSeeder : ISeeder
{
    public async Task SeedAsync(SmartSalonDbContext dbContext, IServiceProvider serviceProvider)
    {
        var workingTime = await dbContext.WorkingTimes.FirstAsync();

        await dbContext.Salons.AddAsync(new()
        {
            Name = "Cosa Nostra",
            Description = "Description",
            Location = "Location",
            DefaultTimePenalty = 5,
            DefaultBookingsInAdvance = 5,
            SubscriptionsEnabled = true,
            WorkersCanMoveBookings = true,
            WorkersCanSetNonWorkingPeriods = true,
            WorkingTimeId = workingTime.Id,
            MainPictureId = null,
        });

        await dbContext.Salons.AddAsync(new()
        {
            Name = "Gosho shop",
            Description = "Description",
            Location = "Location",
            DefaultTimePenalty = 5,
            DefaultBookingsInAdvance = 5,
            SubscriptionsEnabled = true,
            WorkersCanMoveBookings = true,
            WorkersCanSetNonWorkingPeriods = true,
            WorkingTimeId = workingTime.Id,
            MainPictureId = null,
        });
    }
}

