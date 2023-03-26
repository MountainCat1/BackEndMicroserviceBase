using Microsoft.EntityFrameworkCore;

namespace BaseApi.Infrastructure.Contexts;

public class BaseApiDbContext : DbContext
{
    public BaseApiDbContext(DbContextOptions<BaseApiDbContext> options) : base(options)
    {
        
    }
}