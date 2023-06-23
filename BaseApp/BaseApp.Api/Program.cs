using BaseApp.Api;
using BaseApp.Api.Extensions;
using BaseApp.Api.MediaRBehaviors;
using BaseApp.Api.Middlewares;
using BaseApp.Application;
using BaseApp.Application.Configuration;
using BaseApp.Application.Services;
using BaseApp.Infrastructure.Abstractions;
using BaseApp.Infrastructure.Contexts;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ========= CONFIGURATION  =========
var configuration = builder.Configuration;

configuration.AddJsonFile("Secrets/jwt.json");

var jwtConfig = configuration.GetConfiguration<JwtConfig>();

// ========= SERVICES  =========
var services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen(o =>
{
    o.AddSwaggerAuthUi();
});
services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddConsole();
    loggingBuilder.AddDebug();
    loggingBuilder.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);
});

services.AddCors(options =>
{
    options.AddPolicy("AllowOrigins", builder =>
    {
        builder.WithOrigins(new[]
            {
                "http://localhost:4200", // local frontend
                "https://localhost:5000", // local swagger 
                "http://localhost:4000", // local swagger
            })
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

services.AddAsymmetricAuthentication(jwtConfig);

services.AddDbContext<BaseAppDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("BaseAppDatabase"),
        b => b.MigrationsAssembly(typeof(ApiAssemblyMarker).Assembly.FullName));
    options.UseLoggerFactory(LoggerFactory.Create(builder => builder
        .AddFilter((_, _) => false)
        .AddConsole()));
});

services.AddHttpContextAccessor();
services.AddTransient<IUserAccessor, UserAccessor>();
services.AddScoped<IDatabaseErrorMapper, DatabaseErrorMapper>();
services.AddScoped<ErrorHandlingMiddleware>();
services.AddFluentValidationAutoValidation();
services.AddValidatorsFromAssemblyContaining<ApplicationAssemblyMarker>();
services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(ApplicationAssemblyMarker).Assembly));
services.AddAuthorizationHandlers();

// ========= RUN  =========
var app = builder.Build();

if (!app.Environment.IsDevelopment())
    await app.MigrateDatabaseAsync<BaseAppDbContext>();

if (app.Environment.IsDevelopment() || app.Configuration.GetValue<bool>("ENABLE_SWAGGER"))
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");

        // Enable JWT authentication in Swagger UI
        c.OAuthClientId("swagger");
        c.OAuthAppName("Swagger UI");
    });
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseCors("AllowOrigins");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();