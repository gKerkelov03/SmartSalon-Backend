
using Microsoft.Extensions.DependencyInjection;
using SmartSalon.Application.Domain.Users;

namespace SmartSalon.Data.Seeding;

internal class SalonsSeeder : ISeeder
{
    public async Task SeedAsync(SmartSalonDbContext dbContext, IServiceProvider serviceProvider)
    {
        var owners = serviceProvider.GetRequiredService<IEfRepository<Owner>>();
        var owner = await owners.FirstOrDefaultAsync(owner => true);

        if (owner is null)
        {
            throw new InvalidOperationException($"{nameof(SalonsSeeder)} failed because there are no owners existing");
        }

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
                Owners = [owner]
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
                Owners = [owner]
            }
        ]);
    }
}

