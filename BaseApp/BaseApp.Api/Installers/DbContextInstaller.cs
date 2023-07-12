using BaseApp.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BaseApp.Api.Installers;

public static class DbContextInstaller
{
    private const string DatabaseConnectionStringKey = "BaseAppDatabase";
    
    public static IServiceCollection InstallDbContext(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<BaseAppDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString(DatabaseConnectionStringKey),
                b =>
                {
                    b.MigrationsAssembly(typeof(ApiAssemblyMarker).Assembly.FullName);
                    b.EnableRetryOnFailure(5, TimeSpan.FromSeconds(5.0), null);
                });
            options.UseLoggerFactory(LoggerFactory.Create(lb => lb
                .AddFilter((_, _) => false)
                .AddConsole()));
        });


        return services;
    }
}