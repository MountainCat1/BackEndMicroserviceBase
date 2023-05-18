using BaseApp.Application;
using BaseApp.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

var services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

if (builder.Environment.IsDevelopment())
    services.AddDbContext<BaseAppDbContext>(options 
        => options.UseSqlServer(configuration.GetConnectionString("BaseAppDatabase")));
else
    services.AddDbContext<BaseAppDbContext>(options 
        => options.UseSqlServer(configuration.GetConnectionString("BaseAppDatabase")));

services.AddMediatR(cfg=>cfg.RegisterServicesFromAssemblies(typeof(ApplicationAssemblyMarker).Assembly));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();