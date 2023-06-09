using Microsoft.AspNetCore.Authorization;

namespace BaseApp.Api.Extensions;

public static class ServiceCollectionAuthorizationExtensions
{
    public static IServiceCollection AddAuthorizationHandlers(this IServiceCollection services)
    {
        // Here add all authoriztion handlers
        // Example:
        //      services.AddSingleton<IAuthorizationHandler, SomeFunnyAuthorizationHandler>();
        
        return services;
    }
}