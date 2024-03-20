using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SmartSalon.Application.Domain.Abstractions;

namespace SmartSalon.Data.Repositories;

public class Repository<TEntity> : IEfRepository<TEntity>
    where TEntity : class, IBaseEntity<Id>
{
    private readonly DbContext _dbContext;
    private readonly DbSet<TEntity> _dbSet;

    public Repository(SmartSalonDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<TEntity>();
    }

    public IQueryable<TEntity> All()
        => _dbSet;

    public async Task<TEntity?> GetByIdAsync(Id id)
        => await _dbSet.FindAsync(id);

    public async Task<TEntity?> FirstAsync(Expression<Func<TEntity, bool>> predicate)
        => await _dbSet.FirstOrDefaultAsync(predicate);

    public async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate)
        => await _dbSet.Where(predicate).ToListAsync();

    public async Task AddAsync(TEntity entity)
        => await _dbSet.AddAsync(entity);

    public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        => await _dbSet.AddRangeAsync(entities);

    public async Task RemoveByIdAsync(Id id)
    {
        var entity = await GetByIdAsync(id);

        if (entity is null)
        {
            return;
        }

        _dbSet.Remove(entity);
    }

    public void Remove(TEntity entity)
        => _dbSet.Remove(entity);

    public void RemoveRange(IEnumerable<TEntity> entities)
        => _dbSet.RemoveRange(entities);

    public void Update(TEntity entity)
        => _dbContext.Entry(entity).State = EntityState.Modified;

    public async Task UpdateByIdAsync(Id id, TEntity newEntity)
    {
        var entity = await GetByIdAsync(id);

        if (entity is null)
        {
            return;
        }

        _dbContext.Entry(entity).CurrentValues.SetValues(newEntity);
    }
}