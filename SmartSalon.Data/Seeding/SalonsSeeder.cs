
namespace SmartSalon.Data.Seeding;

internal class SalonsSeeder : ISeeder
{
    public async Task SeedAsync(SmartSalonDbContext dbContext, IServiceProvider serviceProvider)
    {
        await dbContext.Salons.AddRangeAsync([
            new()
            {
                Name = "Cosa Nostra",
                Description = "Description",
                Location = "Location",
                DefaultTimePenalty = 5,
                DefaultBookingsInAdvance = 5,
                SubscriptionsEnabled = true,
                WorkersCanMoveBookings = true,
                WorkersCanSetNonWorkingPeriods = true,
            },
            new()
            {
                Name = "Gosho shop",
                Description = "Description",
                Location = "Location",
                DefaultTimePenalty = 5,
                DefaultBookingsInAdvance = 5,
                SubscriptionsEnabled = true,
                WorkersCanMoveBookings = true,
                WorkersCanSetNonWorkingPeriods = true,
            }
        ]);
    }
}

