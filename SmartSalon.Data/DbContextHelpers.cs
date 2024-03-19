using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using SmartSalon.Application.Domain.Abstractions;
using SmartSalon.Shared.Extensions;

namespace SmartSalon.Data;

internal static class DbContextHelpers
{
    public static void SetDeleteBehaviorToRestrict(IEnumerable<IMutableEntityType> entityTypes)
        => entityTypes
            .SelectMany(entity =>
                entity
                .GetForeignKeys()
                .Where(foreignKey => foreignKey.DeleteBehavior is DeleteBehavior.Cascade))
            .ForEach(foreignKey => foreignKey.DeleteBehavior = DeleteBehavior.Restrict);

    public static void SetIsDeletedQueryFilter<TEntity>(ModelBuilder builder)
        where TEntity : class, IDeletableEntity<Id>
            => builder
                .Entity<TEntity>()
                .HasQueryFilter(entity => !entity.IsDeleted);

    public static void SetupDeletedQueryFilter(
        ModelBuilder builder,
        IEnumerable<IMutableEntityType> entities
    )
    {
        var iDeletableEntity = typeof(IDeletableEntity<Id>);
        var dbContextHelpers = typeof(DbContextHelpers);

        var SetIsDeletedQueryFilterMethodInfo = dbContextHelpers.GetMethod(
            nameof(SetIsDeletedQueryFilter),
            BindingFlags.NonPublic | BindingFlags.Static
        );

        var deletableEntities = entities.Where(entity =>
            entity.ClrType is not null &&
            iDeletableEntity.IsAssignableFrom(entity.ClrType)
        );

        deletableEntities.ForEach(deletableEntityType =>
            //TODO: debug why SetIsDeletedQueryFilterMethodInfo is null sometimes
            SetIsDeletedQueryFilterMethodInfo
                ?.MakeGenericMethod(deletableEntityType.ClrType)
                .Invoke(null, [builder])
        );
    }

    public static void ApplyAuditInfoRules(ChangeTracker changeTracker, Id currentUserId)
        => changeTracker
            .Entries()
            .ForEach(entry =>
            {
                var isDeletableEntity = entry.Entity is IDeletableEntity<Id>;
                var isInDeletedState = entry.State is EntityState.Deleted;

                if (isDeletableEntity && isInDeletedState)
                {
                    var deletableEntity = entry.Entity.CastTo<IDeletableEntity<Id>>();

                    deletableEntity.DeletedBy = currentUserId;
                    deletableEntity.DeletedOn = DateTime.UtcNow;
                    deletableEntity.IsDeleted = true;

                    entry.State = EntityState.Modified;
                }
            });
}
