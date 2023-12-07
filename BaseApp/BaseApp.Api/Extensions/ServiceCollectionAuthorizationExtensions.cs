using BaseApp.Application.Authorization.Handlers;
using Microsoft.AspNetCore.Authorization;

namespace BaseApp.Api.Extensions
{
    public static class ServiceCollectionAuthorizationExtensions
    {
        public static IServiceCollection AddResourceAuthorization(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationHandler, ExampleAuthorizationHandler>();

            return services;
        }
    }
}