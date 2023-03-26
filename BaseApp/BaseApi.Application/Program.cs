using BaseApi.CQRS;
using BaseApi.Infrastructure.Contexts;
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
    services.AddDbContext<BaseApiDbContext>(options => options.UseInMemoryDatabase("BaseApiDatabase"));
else
    services.AddDbContext<BaseApiDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("BaseApiDatabase")));

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