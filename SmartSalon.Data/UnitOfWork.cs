using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Domain.Base;
using SmartSalon.Application.Extensions;

namespace SmartSalon.Data;

public class UnitOfWork(SmartSalonDbContext _dbContext, ICurrentUserAccessor _currentUser, TimeProvider _timeProvider) : IUnitOfWork
{
    private IDbContextTransaction? _transaction;

    public Task<int> SaveAsync(CancellationToken cancellationToken = default)
    {
        //TODO:fix this 
        // if (_currentUser.Id is null)
        // {
        //     throw new InvalidOperationException("Current user's id is null when it shouldn't be!");
        // }

        var userIdNonNullable = new Id("ec05c91b-b11d-41a6-e732-08dc549969d9");//think _currentUser.Id.CastTo<Id>();
        ApplyAuditInfoRules(_dbContext.ChangeTracker, userIdNonNullable);

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

    public void ApplyAuditInfoRules(ChangeTracker changeTracker, Id currentUserId)
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

                deletableEntity.IsDeleted = true;
                deletableEntity.DeletedBy = currentUserId;
                deletableEntity.DeletedOn = _timeProvider.GetUtcNow();

                entry.State = EntityState.Modified;
            });
}
