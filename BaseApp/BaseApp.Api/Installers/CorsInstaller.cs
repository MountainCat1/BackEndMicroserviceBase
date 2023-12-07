namespace BaseApp.Api.Installers
{
    public static class CorsInstaller 
    {
        public static IServiceCollection InstallCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowOrigins", policyBuilder =>
            {
                policyBuilder.WithOrigins(new[]
                    {
                        "http://localhost:4200", // local frontend
                        "https://localhost:5000", // local swagger 
                        "http://localhost:4000", // local swagger
                    })
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        return services;
    }
    }
}