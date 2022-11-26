using AppApi.CQRS;
using AppApi.Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

var services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

if (builder.Environment.IsDevelopment())
    services.AddDbContext<BaseDbContext>(options => options.UseInMemoryDatabase("BaseDatabase"));
else
    services.AddDbContext<BaseDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("BaseDatabase")));

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