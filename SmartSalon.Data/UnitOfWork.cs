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

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entriesForSoftDelete = _dbContext
            .ChangeTracker
            .Entries()
            .Where(entry => 
                entry.Entity is IDeletableEntity &&
                entry.State is EntityState.Deleted
            );

        if (entriesForSoftDelete.Any())
        {
            ApplySoftDelete(entriesForSoftDelete);
        }

        return await _dbContext.SaveChangesAsync(cancellationToken);
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

    public void ApplySoftDelete(IEnumerable<EntityEntry> entries)
        => entries.ForEach(entry =>
        {
            var deletableEntity = entry.Entity.CastTo<IDeletableEntity>();

            deletableEntity.IsDeleted = true;
            deletableEntity.DeletedBy = _currentUser.Id;
            deletableEntity.DeletedOn = _timeProvider.GetUtcNow().LocalDateTime;

            entry.State = EntityState.Modified;
        });
}
