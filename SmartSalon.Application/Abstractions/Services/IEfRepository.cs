using System.Linq.Expressions;
using SmartSalon.Application.Abstractions.Lifetime;
using SmartSalon.Application.Domain.Base;
using SmartSalon.Application.ResultObject;

public interface IEfRepository<TEntity> : IScopedLifetime where TEntity : IBaseEntity
{
    Task AddAsync(TEntity entity);
    Task AddRangeAsync(IEnumerable<TEntity> entities);

    IQueryable<TEntity> All { get; }
    Task<TEntity?> GetByIdAsync(Id id);
    Task<TEntity?> FirstAsync(Expression<Func<TEntity, bool>> predicate);
    Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate);

    void Update(TEntity newEntity);
    Task<Result> UpdateByIdAsync(Id id, TEntity newEntity);

    Task<Result> RemoveByIdAsync(Id id);
}