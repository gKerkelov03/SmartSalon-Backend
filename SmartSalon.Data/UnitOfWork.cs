using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Domain.Abstractions;
using SmartSalon.Application.Extensions;

namespace SmartSalon.Data;

public class UnitOfWork(SmartSalonDbContext _dbContext, ICurrentUserAccessor _currentUser) : IUnitOfWork
{
    private IDbContextTransaction? _transaction;

    public Task<int> SaveAsync(CancellationToken cancellationToken = default)
    {
        if (_currentUser.Id is null)
        {
            throw new InvalidOperationException("Current user's id is null when it shouldn't be!");
        }

        var UserIdNonNullable = _currentUser.Id.CastTo<Id>();
        ApplyAuditInfoRules(_dbContext.ChangeTracker, UserIdNonNullable);

        return _dbContext.SaveChangesAsync(cancellationToken);
    }

    public void BeginTransaction()
        => _transaction = _dbContext.Database.BeginTransaction();

    public async Task CommitTransactionAsync()
    {
        if (_transaction is null)
        {
            throw new InvalidOperationException("You cannot commit a transaction before it has been started.");
        }

        await _dbContext.SaveChangesAsync();
        await _transaction.CommitAsync();

        _transaction = null;
    }

    public void RollbackTransaction()
    {
        if (_transaction is null)
        {
            throw new InvalidOperationException("You cannot rollback a transaction before it has been started.");
        }

        _transaction.Rollback();
    }

    public static void ApplyAuditInfoRules(ChangeTracker changeTracker, Id currentUserId)
        => changeTracker
            .Entries()
            .ForEach(entry =>
            {
                var isNotDeletableEntity = entry.Entity is not IDeletableEntity;
                var isNotInDeletedState = entry.State is not EntityState.Deleted;

                if (isNotDeletableEntity || isNotInDeletedState)
                {
                    return;
                }

                var deletableEntity = entry.Entity.CastTo<IDeletableEntity>();

                deletableEntity.DeletedBy = currentUserId;
                deletableEntity.DeletedOn = DateTime.UtcNow;
                deletableEntity.IsDeleted = true;

                entry.State = EntityState.Modified;
            });
}
