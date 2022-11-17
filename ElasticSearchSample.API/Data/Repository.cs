using ElasticSearchSample.API.Abstractions;
using ElasticSearchSample.API.Models;

using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;

namespace ElasticSearchSample.API.Data;

public class Repository<TEntity> : IDataRepository<TEntity> where TEntity : BaseEntity
{
    private readonly ApplicationDbContext _dbContext;

    public Repository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TEntity?> AddAsync(TEntity entity)
    {
        var newEntity = await _dbContext.AddAsync<TEntity>(entity);
        var isAdded = await _dbContext.SaveChangesAsync() > 0;
        if (isAdded)
        {
            return newEntity.Entity;
        }
        return null;
    }

    public async Task<TEntity?> GetAsync(Guid id)
    {
        return await _dbContext.Set<TEntity>().SingleOrDefaultAsync(x=> x.Id.Equals(id));
    }

    public async Task<IEnumerable<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await _dbContext.Set<TEntity>().Where(expression).ToListAsync();
    }

    public async Task<TEntity?> UpdateAsync(TEntity entity)
    {
        var updatedEntity = _dbContext.Update<TEntity>(entity);
        var isChanged = await _dbContext.SaveChangesAsync() > 0;
        if (isChanged)
        {
            return updatedEntity.Entity;
        }
        return null;
    }
}
