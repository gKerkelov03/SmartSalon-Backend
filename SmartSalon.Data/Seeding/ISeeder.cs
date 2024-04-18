namespace SmartSalon.Data.Seeding;

public interface ISeeder
{
    Task SeedAsync(SmartSalonDbContext dbContext, IServiceProvider serviceProvider);
}