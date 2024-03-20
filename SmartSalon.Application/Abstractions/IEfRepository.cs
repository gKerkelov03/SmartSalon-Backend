using System.Linq.Expressions;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Domain.Abstractions;

public interface IEfRepository<TEntity> : IScopedLifetime
    where TEntity : IBaseEntity<Id>
{
    IQueryable<TEntity> All();

    Task<TEntity?> GetByIdAsync(Id id);

    Task<TEntity?> FirstAsync(Expression<Func<TEntity, bool>> predicate);

    Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate);

    Task AddAsync(TEntity entity);

    Task AddRangeAsync(IEnumerable<TEntity> entities);

    void Remove(TEntity entity);

    Task RemoveByIdAsync(Id id);

    void RemoveRange(IEnumerable<TEntity> entities);

    Task UpdateByIdAsync(Id id, TEntity newEntity);

    void Update(TEntity entity);
}