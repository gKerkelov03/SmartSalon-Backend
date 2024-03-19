
using System.Linq.Expressions;
using SmartSalon.Application.Domain.Abstractions;

public interface IEfRepository<TEntity> where TEntity : IBaseEntity<Id>
{
    IQueryable<TEntity> All();

    Task<TEntity?> GetByIdAsync(Id id);

    Task<TEntity?> FirstAsync(Expression<Func<TEntity, bool>> predicate);

    Task<IEnumerable<TEntity?>> FindAllAsync(Expression<Func<TEntity, bool>> predicate);

    Task AddAsync(TEntity entity);

    Task AddRangeAsync(IEnumerable<TEntity> entities);

    Task RemoveByIdAsync(Id id);

    Task RemoveAsync(TEntity entity);

    Task RemoveRangeAsync(IEnumerable<TEntity> entities);

    Task UpdateById(Id id, TEntity newEntity);

    Task UpdateAsync(TEntity entity);
}