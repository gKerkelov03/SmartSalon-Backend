using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SmartSalon.Data.Entities.Users;
using SmartSalon.Data.Entities.Bookings;
using SmartSalon.Data.Entities.Subscriptions;
using SmartSalon.Data.Entities.Salons;
using static SmartSalon.Data.DbContextHelpers;

namespace SmartSalon.Data;

public class SmartSalonDbContext : IdentityDbContext<UserProfile, Role, Id>
{
    public SmartSalonDbContext() { }

    public SmartSalonDbContext(DbContextOptions<SmartSalonDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (options.IsConfigured)
        {
            return;
        }

        var connectionString =
            "Server=.,1433;Database=WrongDatabaseName;TrustServerCertificate=True;User Id=sa;Password=P@ssw0rd123";

        options.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        var entityTypes = builder
            .Model
            .GetEntityTypes();

        base.OnModelCreating(builder);

        SetDeleteBehaviorToRestrict(entityTypes);
        SetupDeletedQueryFilter(builder, entityTypes);
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        ApplyAuditInfoRules(this.ChangeTracker);

        var totalNumberOfChangesMade =
            base.SaveChanges(acceptAllChangesOnSuccess);

        return totalNumberOfChangesMade;
    }

    public override Task<int> SaveChangesAsync(
        bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default
    )
    {
        ApplyAuditInfoRules(this.ChangeTracker);

        var totalNumberOfChangesMade =
            base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

        return totalNumberOfChangesMade;
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
