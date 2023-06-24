using BaseApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BaseApp.Infrastructure.Contexts;

public class BaseAppDbContext : DbContext
{
    public SomeEntity SomeEntity { get; set; }
    
    public BaseAppDbContext(DbContextOptions<BaseAppDbContext> options) : base(options)
    {
        
    }
}