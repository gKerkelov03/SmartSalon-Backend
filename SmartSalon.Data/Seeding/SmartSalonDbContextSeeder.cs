using SmartSalon.Shared.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SmartSalon.Application.Extensions;

namespace SmartSalon.Data.Seeding;

public class SmartSalonDbContextSeeder : ISeeder
{
    public async Task SeedAsync(SmartSalonDbContext dbContext, IServiceProvider serviceProvider)
    {
        ArgumentNullException.ThrowIfNull(serviceProvider);

        var smartSalonDbContextSeeder = typeof(SmartSalonDbContextSeeder)!;
        var logger = serviceProvider
            .GetService<ILoggerFactory>()
            ?.CreateLogger(smartSalonDbContextSeeder);

        await GetAllSeeders().ForEachAsync(async seeder =>
        {
            await seeder.SeedAsync(dbContext, serviceProvider);
            await dbContext.SaveChangesAsync();

            var seederName = seeder.GetType().Name;
            logger?.LogInformation($"Seeder {seederName} done.");
        });
    }

    private static IEnumerable<ISeeder> GetAllSeeders()
        => typeof(SmartSalonDbContext)
            .Assembly
            .GetTypes()
            .Where(type =>
                typeof(ISeeder).IsBaseTypeOf(type) &&
                type.IsNotAbsctractOrInterface()
            )
            .Select(seederType => Activator
                .CreateInstance(seederType)
                !.CastTo<ISeeder>()
            );

}
