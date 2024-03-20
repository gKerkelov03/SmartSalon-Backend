using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Domain.Abstractions;
using SmartSalon.Application.Services;
using SmartSalon.Shared.Extensions;

namespace SmartSalon.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly SmartSalonDbContext _dbContext;
    private readonly ICurrentUserAccessor _currentUser;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(SmartSalonDbContext dbContext, ICurrentUserAccessor currentUser)
    {
        _dbContext = dbContext;
        _currentUser = currentUser;
    }

    public int SaveChanges()
    {
        ApplyAuditInfoRules(_dbContext.ChangeTracker, _currentUser.Id);
        return _dbContext.SaveChanges();
    }

    public Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default
    )
    {
        ApplyAuditInfoRules(_dbContext.ChangeTracker, _currentUser.Id);
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
                var isNotDeletableEntity = entry.Entity is not IDeletableEntity<Id>;
                var isNotInDeletedState = entry.State is not EntityState.Deleted;

                if (isNotDeletableEntity || isNotInDeletedState)
                {
                    return;
                }

                var deletableEntity = entry.Entity.CastTo<IDeletableEntity<Id>>();

                deletableEntity.DeletedBy = currentUserId;
                deletableEntity.DeletedOn = DateTime.UtcNow;
                deletableEntity.IsDeleted = true;

                entry.State = EntityState.Modified;
            });
}
