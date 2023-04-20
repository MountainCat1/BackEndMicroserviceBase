using BaseApp.Application;
using BaseApp.Infrastructure.Contexts;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

var services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

if (builder.Environment.IsDevelopment())
    services.AddDbContext<BaseAppDbContext>(options 
        => options.UseSqlite(configuration.GetConnectionString("BaseAppDatabase")));
else
    services.AddDbContext<BaseAppDbContext>(options 
        => options.UseSqlServer(configuration.GetConnectionString("BaseAppDatabase")));

services.AddMediatR(typeof(CqrsAssemblyMarker));

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