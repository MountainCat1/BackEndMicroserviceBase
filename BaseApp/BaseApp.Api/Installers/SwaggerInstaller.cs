using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BaseApp.Api.Installers;

public static class SwaggerInstaller
{
    public static IServiceCollection InstallSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(o =>
        {
            o.AddSwaggerAuthUi();
        });

        return services;
    }
    
    public static SwaggerGenOptions AddSwaggerAuthUi(this SwaggerGenOptions options)
    {
        // Add the security definition for JWT Bearer authentication
        var securityScheme = new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Description = "Enter 'Bearer {token}'",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        };

        options.AddSecurityDefinition("Bearer", securityScheme);

        // Apply the security requirement globally to all endpoints
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                securityScheme, new List<string>()
            }
        });

        return options;
    }
}