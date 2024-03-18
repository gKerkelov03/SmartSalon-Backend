using SmartSalon.Shared.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;

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

        await this.GetAllSeeders().ForEachAsync(async seeder =>
        {
            await seeder.SeedAsync(dbContext, serviceProvider);
            await dbContext.SaveChangesAsync();

            logger?.LogInformation($"Seeder {seeder.GetType().Name} done.");
        });
    }

    private IEnumerable<ISeeder> GetAllSeeders()
        => Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(type =>
                typeof(ISeeder).IsBaseTypeOf(type) &&
                type.IsNotAbsctractOrInterface()
            ).Select(seederType => Activator
                .CreateInstance(seederType)
                !.CastTo<ISeeder>()
            );

}
