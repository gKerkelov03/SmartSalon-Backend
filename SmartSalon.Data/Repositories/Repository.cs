using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SmartSalon.Application.Domain.Abstractions;

namespace SmartSalon.Data.Repositories;

public class Repository<TEntity> : IEfRepository<TEntity>
    where TEntity : class, IBaseEntity<Id>
{
    private readonly DbContext dbContext;
    private readonly DbSet<TEntity> dbSet;

    public Repository(SmartSalonDbContext dbContext)
    {
        this.dbContext = dbContext;
        this.dbSet = dbContext.Set<TEntity>();
    }

    public IQueryable<TEntity> All()
        => dbSet;

    public async Task<TEntity?> GetByIdAsync(Id id)
        => await dbSet.FindAsync(id);

    public async Task<TEntity?> FirstAsync(Expression<Func<TEntity, bool>> predicate)
        => await dbSet.FirstOrDefaultAsync(predicate);

    public async Task<IEnumerable<TEntity?>> FindAllAsync(Expression<Func<TEntity, bool>> predicate)
        => await dbSet.Where(predicate).ToListAsync();

    public async Task AddAsync(TEntity entity)
    {
        await dbSet.AddAsync(entity);
        await this.dbContext.SaveChangesAsync();
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        await dbSet.AddRangeAsync(entities);
        await this.dbContext.SaveChangesAsync();
    }

    public async Task RemoveByIdAsync(Id id)
    {
        var entity = await this.GetByIdAsync(id);

        if (entity is null)
        {
            return;
        }

        dbSet.Remove(entity);
        await this.dbContext.SaveChangesAsync();
    }

    public async Task RemoveAsync(TEntity entity)
    {
        dbSet.Remove(entity);
        await this.dbContext.SaveChangesAsync();
    }

    public async Task RemoveRangeAsync(IEnumerable<TEntity> entities)
    {
        dbSet.RemoveRange(entities);
        await this.dbContext.SaveChangesAsync();
    }

    public async Task UpdateById(Id id, TEntity newEntity)
    {
        var entity = await GetByIdAsync(id);

        if (entity is null)
        {
            return;
        }

        this.dbContext.Entry(entity).CurrentValues.SetValues(newEntity);
        await this.dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        this.dbContext.Entry(entity).State = EntityState.Modified;
        await this.dbContext.SaveChangesAsync();
    }
}