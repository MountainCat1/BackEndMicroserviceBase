using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BaseApp.Domain.Abstractions;


public interface IRepository
{
    
}
public interface IRepository<TEntity> : IRepository where TEntity : class, IEntity
{
    public Task SaveChangesAsync();
}