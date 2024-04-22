
namespace SmartSalon.Data.Seeding;

internal class CurrenciesSeeder : ISeeder
{
    public async Task SeedAsync(SmartSalonDbContext dbContext, IServiceProvider serviceProvider)
    {
        await dbContext.Currencies.AddRangeAsync([
            new()
            {
                Name = "Bulgarian Lev",
                Code = "BGN",
                Country = "Bulgaria",
            },
            new()
            {
                Name = "Euro",
                Code = "EUR",
                Country = "European Union",
            },
            new()
            {
                Name = "US Dollar",
                Code = "USD",
                Country = "United States of America",
            },
        ]);
    }
}