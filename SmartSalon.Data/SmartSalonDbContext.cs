using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SmartSalon.Application.Domain;
using Microsoft.EntityFrameworkCore.Metadata;
using SmartSalon.Application.Domain.Abstractions;
using SmartSalon.Shared.Extensions;
using System.Reflection;

namespace SmartSalon.Data;

public class SmartSalonDbContext : IdentityDbContext<UserProfile, Role, Id>
{
    public SmartSalonDbContext()
    {
    }

    public SmartSalonDbContext(DbContextOptions<SmartSalonDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (options.IsConfigured)
        {
            return;
        }

        var connectionString = "Server=.,1433;Database=WrongDatabaseName;TrustServerCertificate=True;User Id=sa;Password=P@ssw0rd123";

        options.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        var entityTypes = builder
            .Model
            .GetEntityTypes();

        builder.ApplyConfigurationsFromAssembly(typeof(SmartSalonDbContext).Assembly);
        base.OnModelCreating(builder);

        SetDeleteBehaviorToRestrict(entityTypes);
        SetupDeletedQueryFilter(builder, entityTypes);
    }


    private static void SetDeleteBehaviorToRestrict(IEnumerable<IMutableEntityType> entityTypes)
        => entityTypes
            .SelectMany(entity =>
                entity
                .GetForeignKeys()
                .Where(foreignKey => foreignKey.DeleteBehavior is DeleteBehavior.Cascade))
            .ForEach(foreignKey => foreignKey.DeleteBehavior = DeleteBehavior.Restrict);

    private static void SetIsDeletedQueryFilter<TEntity>(ModelBuilder builder)
        where TEntity : class, IDeletableEntity<Id>
            => builder
                .Entity<TEntity>()
                .HasQueryFilter(entity => !entity.IsDeleted);

    private static void SetupDeletedQueryFilter(
        ModelBuilder builder,
        IEnumerable<IMutableEntityType> entities
    )
    {
        var SetIsDeletedQueryFilterMethodInfo = typeof(UnitOfWork).GetMethod(
            nameof(SetIsDeletedQueryFilter),
            BindingFlags.NonPublic | BindingFlags.Static
        );

        var deletableEntities = entities.Where(entity =>
            entity.ClrType is not null &&
            typeof(IDeletableEntity<Id>).IsAssignableFrom(entity.ClrType)
        );

        deletableEntities.ForEach(deletableEntityType =>
            //TODO: debug why SetIsDeletedQueryFilterMethodInfo is null sometimes
            SetIsDeletedQueryFilterMethodInfo
                ?.MakeGenericMethod(deletableEntityType.ClrType)
                .Invoke(null, [builder])
        );
    }

    public DbSet<Owner> Owners { get; set; }

    public DbSet<Worker> Workers { get; set; }

    public DbSet<Customer> Customers { get; set; }

    public DbSet<Salon> Salons { get; set; }

    public DbSet<SalonService> SalonServices { get; set; }

    public DbSet<SalonSpecialty> SalonSpecialties { get; set; }

    public DbSet<SpecialSlot> SpecialSlots { get; set; }

    public DbSet<Subscription> Subscriptions { get; set; }

    public DbSet<Booking> Bookings { get; set; }

    public DbSet<BookingTime> BookingTimes { get; set; }
}
