﻿using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SmartSalon.Application.Domain;
using Microsoft.EntityFrameworkCore.Metadata;
using SmartSalon.Application.Domain.Base;
using System.Reflection;
using SmartSalon.Application.Extensions;
using Microsoft.AspNetCore.Identity;
using SmartSalon.Application.Domain.Users;

namespace SmartSalon.Data;

public class SmartSalonDbContext : IdentityDbContext<User, Role, Id>
{
    public SmartSalonDbContext() { }

    public SmartSalonDbContext(DbContextOptions<SmartSalonDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (options.IsConfigured)
        {
            return;
        }

        options.UseSqlServer("Server=.,1433;Database=WrongDatabaseName;TrustServerCertificate=True;User Id=sa;Password=P@ssw0rd123");
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        var entityTypes = builder.Model.GetEntityTypes();

        builder
            .Ignore<IdentityUserClaim<Id>>()
            .Ignore<IdentityRoleClaim<Id>>()
            .Ignore<IdentityUserToken<Id>>()
            .ApplyConfigurationsFromAssembly(typeof(SmartSalonDbContext).Assembly);

        SetDeleteBehaviorToRestrict(entityTypes);
        SetupDeletedQueryFilter(builder, entityTypes);
    }

    private static void SetDeleteBehaviorToRestrict(IEnumerable<IMutableEntityType> entityTypes)
        => entityTypes.SelectMany(entity =>
                entity.GetForeignKeys().Where(foreignKey => foreignKey.DeleteBehavior is not DeleteBehavior.Restrict)
            )
            .ForEach(foreignKey => foreignKey.DeleteBehavior = DeleteBehavior.Restrict);

    private static void DontShowIfDeleted<TEntity>(ModelBuilder builder)
        where TEntity : class, IDeletableEntity
            => builder.Entity<TEntity>().HasQueryFilter(entity => !entity.IsDeleted);

    private static void SetupDeletedQueryFilter(ModelBuilder builder, IEnumerable<IMutableEntityType> entities)
    {
        var dontShowIfDeleted = typeof(UnitOfWork).GetMethod(
            nameof(DontShowIfDeleted),
            BindingFlags.NonPublic | BindingFlags.Static
        );

        entities
            .Where(entity =>
                entity.ClrType is not null &&
                typeof(IDeletableEntity).IsAssignableFrom(entity.ClrType)
            )
            .ForEach(deletableEntityType =>
                //TODO: debug why SetIsDeletedQueryFilterMethodInfo is null sometimes
                dontShowIfDeleted?.MakeGenericMethod(deletableEntityType.ClrType).Invoke(null, [builder])
            );
    }

    public new DbSet<User> Users { get; set; }
    public new DbSet<Role> Roles { get; set; }
    public DbSet<Owner> Owners { get; set; }
    public DbSet<Worker> Workers { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Salon> Salons { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Specialty> SalonSpecialties { get; set; }
    public DbSet<SpecialSlot> SpecialSlots { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<TimePeriod> TimePeriods { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Token> Tokens { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Section> Sections { get; set; }
    public DbSet<WorkingTime> WorkingTimes { get; set; }
}
