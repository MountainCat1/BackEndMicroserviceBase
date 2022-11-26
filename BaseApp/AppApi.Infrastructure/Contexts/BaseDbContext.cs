using Microsoft.EntityFrameworkCore;

namespace AppApi.Infrastructure.Contexts;

public class BaseDbContext : DbContext
{
    public BaseDbContext(DbContextOptions<BaseDbContext> options) : base(options)
    {
        
    }
}