using Microsoft.EntityFrameworkCore;

namespace BaseApp.Infrastructure.Contexts;

public class BaseAppDbContext : DbContext
{
    public BaseAppDbContext(DbContextOptions<BaseAppDbContext> options) : base(options)
    {
        
    }
}