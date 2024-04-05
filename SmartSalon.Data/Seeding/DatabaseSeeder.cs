﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SmartSalon.Data.Seeding;

public class DatabaseSeeder : ISeeder
{
    public async Task SeedAsync(SmartSalonDbContext dbContext, IServiceProvider serviceProvider)
    {
        ArgumentNullException.ThrowIfNull(serviceProvider);

        var seeders = new ISeeder[]
        {
            new RolesSeeder(),
            new SalonsSeeder(),
            new UsersSeeder()
        };

        var logger = serviceProvider.GetService<ILogger<DatabaseSeeder>>();

        foreach (var seeder in seeders)
        {
            var seederName = seeder.GetType().Name;

            try
            {
                await seeder.SeedAsync(dbContext, serviceProvider);
                await dbContext.SaveChangesAsync();
                logger?.LogInformation($"Seeder {seederName} done.");
            }
            catch
            {
                logger?.LogError($"Seeder {seederName} failed.");
            }
        };
    }
}
