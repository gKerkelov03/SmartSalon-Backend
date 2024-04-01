
using SmartSalon.Application.Abstractions.Lifetimes;

namespace SmartSalon.Application.Abstractions;

public interface IUnitOfWork : IScopedLifetime
{
    Task<int> SaveAsync(CancellationToken cancellationToken);

    void BeginTransaction();

    Task CommitTransactionAsync();

    void RollbackTransaction();
}
