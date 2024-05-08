using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SmartSalon.Application.Domain.Base;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Data.Repositories;

public class Repository<TEntity>(SmartSalonDbContext _dbContext) : IEfRepository<TEntity>
    where TEntity : class, IBaseEntity
{
    private readonly DbSet<TEntity> _dbSet = _dbContext.Set<TEntity>();

    public IQueryable<TEntity> All => _dbSet;

    public async Task<TEntity?> GetByIdAsync(Id id) => await _dbSet.FindAsync(id);

    public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        => await _dbSet.FirstOrDefaultAsync(predicate);

    public TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        => _dbSet.FirstOrDefault(predicate);

    public async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate)
        => await _dbSet.Where(predicate).ToListAsync();

    public async Task AddAsync(TEntity entity) => await _dbSet.AddAsync(entity);

    public async Task AddRangeAsync(IEnumerable<TEntity> entities) => await _dbSet.AddRangeAsync(entities);

    public async Task<Result> RemoveByIdAsync(Id id)
    {
        var entity = await GetByIdAsync(id);

        if (entity is null)
        {
            return Error.NotFound;
        }

        _dbSet.Remove(entity);

        return Result.Success();
    }

    public async Task<Result> UpdateByIdAsync(Id id, TEntity newEntity)
    {
        var entity = await GetByIdAsync(id);

        if (entity is null)
        {
            return Error.NotFound;
        }

        _dbContext.Entry(entity).CurrentValues.SetValues(newEntity);
        return Result.Success();
    }

    public void Update(TEntity newEntity) => _dbSet.Update(newEntity);
}