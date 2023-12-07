using BaseApp.Infrastructure.Contexts;
using Catut.Application.Abstractions;

namespace BaseApp.Application.Services;

// TODO: Replace placeholder
public interface ISomeEntityUnitOfWork : IUnitOfWork<BaseAppDbContext>
{
}

public class SomeEntityUnitOfWork : UnitOfWork<BaseAppDbContext>, ISomeEntityUnitOfWork
{
    public SomeEntityUnitOfWork(BaseAppDbContext context) : base(context)
    {
    }
}

