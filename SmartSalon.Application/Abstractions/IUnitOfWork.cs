
namespace SmartSalon.Application.Abstractions;

public interface IUnitOfWork : IScopedLifetime
{
    int SaveChanges();

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    void BeginTransaction();

    Task CommitTransactionAsync();

    void RollbackTransaction();
}
