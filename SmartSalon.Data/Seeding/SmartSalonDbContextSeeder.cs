using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SmartSalon.Data.Seeding.Seeders;

namespace SmartSalon.Data.Seeding;

public class SmartSalonDbContextSeeder : ISeeder
{
    public async Task SeedAsync(SmartSalonDbContext dbContext, IServiceProvider serviceProvider)
    {
        ArgumentNullException.ThrowIfNull(serviceProvider);

        var logger = serviceProvider
            .GetService<ILogger<SmartSalonDbContextSeeder>>();

        foreach (var seeder in GetAllSeeders())
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

    private static IEnumerable<ISeeder> GetAllSeeders()
    {
        return [new RolesSeeder(), new UsersSeeder()];
        // var mainSeeder = typeof(SmartSalonDbContextSeeder);

        // return mainSeeder
        //         .Assembly
        //         .GetTypes()
        //         .Where(type =>
        //             typeof(ISeeder).IsAssignableFrom(type) &&
        //             type.IsNotAbsctractOrInterface() &&
        //             type != mainSeeder)
        //         .Select(seederType => Activator
        //             .CreateInstance(seederType)
        //             !.CastTo<ISeeder>()
        //         );
    }
}
