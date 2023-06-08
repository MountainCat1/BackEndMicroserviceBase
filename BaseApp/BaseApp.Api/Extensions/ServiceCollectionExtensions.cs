using System.Security.Cryptography;
using BaseApp.Application.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BaseApp.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAsymmetricAuthentication(this IServiceCollection services, JwtConfig jwtConfig)
    {
        services.AddSingleton<RsaSecurityKey>(provider =>
        {
            // It's required to register the RSA key with depedency injection.
            // If you don't do this, the RSA instance will be prematurely disposed.

            RSA rsa = RSA.Create();
            rsa.ImportRSAPublicKey(
                source: Convert.FromBase64String(jwtConfig.PublicKey),
                bytesRead: out int _
            );

            return new RsaSecurityKey(rsa);
        });
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                SecurityKey key = services.BuildServiceProvider().GetRequiredService<RsaSecurityKey>();

                options.IncludeErrorDetails = true; // <- great for debugging

                // Configure the actual Bearer validation
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = key,
                    ValidAudience = jwtConfig.Audience,
                    ValidIssuer = jwtConfig.Issuer,
                    RequireSignedTokens = true,
                    RequireExpirationTime = true, // <- JWTs are required to have "exp" property set
                    ValidateLifetime = true, // <- the "exp" will be validated
                    ValidateAudience = true,
                    ValidateIssuer = true,
                };
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