using System.Linq.Expressions;
using BaseApp.Domain.Abstractions;
using BaseApp.Infrastructure.Abstractions;
using BaseApp.Infrastructure.Errors.Database;
using BaseApp.Infrastructure.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BaseApp.Infrastructure.Generics;

public class Repository<TEntity, TDbContext> : IRepository<TEntity>
    where TEntity : Entity
    where TDbContext : DbContext
{
    private readonly DbSet<TEntity> _dbSet;
    private readonly Func<Task> _saveChangesAsyncDelegate;
    private IMediator _mediator;
    private TDbContext _dbContext;
    private ILogger<Repository<TEntity, TDbContext>> _logger;
    private IDatabaseErrorMapper _databaseErrorMapper;


    public Repository(
        TDbContext dbContext,
        IMediator mediator,
        ILogger<Repository<TEntity, TDbContext>> logger,
        IDatabaseErrorMapper databaseErrorMapper)
    {
        _dbContext = dbContext;
        _mediator = mediator;
        _logger = logger;
        _databaseErrorMapper = databaseErrorMapper;
        _dbSet = dbContext.Set<TEntity>();

        _saveChangesAsyncDelegate = async () => { await dbContext.SaveChangesAsync(); };
    }

    public virtual async Task<TEntity?> GetOneAsync(params object[] guids)
    {
        if (guids.Length == 0)
            throw new ArgumentException("No key provided");

        var entity = await _dbSet.FindAsync(guids);

        return entity;
    }

    public virtual async Task<IEnumerable<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        params string[] includeProperties)
    {
        IQueryable<TEntity> query = _dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties)
        {
            query = query.Include(includeProperty);
        }

        if (orderBy != null)
        {
            return await orderBy(query).ToListAsync();
        }
        else
        {
            return await query.ToListAsync();
        }
    }

    public async Task<TEntity?> GetOneAsync(Expression<Func<TEntity, bool>>? filter = null,
        params string[] includeProperties)
    {
        IQueryable<TEntity> query = _dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties)
        {
            query = query.Include(includeProperty);
        }

        return await query.FirstOrDefaultAsync();
    }
    
    public async Task<TEntity> GetOneRequiredAsync(Expression<Func<TEntity, bool>>? filter = null,
        params string[] includeProperties)
    {
        var entity = await GetOneAsync(filter, includeProperties);

        if (entity == null)
            throw new ItemNotFoundException();

        return entity;
    }

    public virtual async Task<TEntity> GetOneRequiredAsync(params object[] guids)
    {
        var entity = await GetOneAsync(guids);

        if (entity == null)
            throw new ItemNotFoundException();

        return entity;
    }

    public virtual async Task<ICollection<TEntity>> GetAllAsync()
    {
        var entities = await _dbSet.ToListAsync();

        return entities;
    }

    public virtual async Task<TEntity> DeleteAsync(TEntity entity)
    {
        _dbSet.Remove(entity);

        return entity;
    }

    public virtual Task<TEntity> AddAsync(TEntity entity)
    {
        _dbSet.Add(entity);

        return Task.FromResult(entity);
    }

    public virtual async Task<TEntity> UpdateAsync(object update, params object[] keys)
    {
        var entity = await GetOneRequiredAsync(keys);

        _dbSet.Attach(entity).CurrentValues.SetValues(update);

        return entity;
    }
    
    public virtual async Task<TEntity> UpdateAsync(TEntity entity)
    {
        _dbSet.Attach(entity).State = EntityState.Modified;

        return entity;
    }

    public virtual async Task SaveChangesAsync()
    {
        try
        {
            await _saveChangesAsyncDelegate();
        }
        catch (DbUpdateException ex) when (ex.IsDuplicateEntryViolation())
        {
            throw new DuplicateEntryException("A duplicate entry was detected.");
        }
        catch (DatabaseException ex)
        {
            _logger.LogError(ex.Message);
            ClearDomainEvents();
            throw;
        }
        
        await _mediator.DispatchDomainEventsAsync(_dbContext);
    }

    private void ClearDomainEvents()
    {
        var domainEntities = _dbContext.ChangeTracker
            .Entries<Entity>()
            .Where(entry => entry.Entity.DomainEvents != null && entry.Entity.DomainEvents.Any())
            .ToArray();

        foreach (var entry in domainEntities)
            entry.Entity.ClearDomainEvents();
    }
}