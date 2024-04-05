
using SmartSalon.Application.Abstractions.Lifetime;

namespace SmartSalon.Application.Abstractions;

public interface IUnitOfWork : IScopedLifetime
{
    Task<int> SaveAsync(CancellationToken cancellationToken);

    void BeginTransaction();

    Task CommitTransactionAsync();

    void RollbackTransaction();
}
