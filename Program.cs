using FileUpload.API.Entities;
using FileUpload.API.Middleware;
using FileUpload.API.Repository.Implements;
using FileUpload.API.Repository.Presistence;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connection = builder.Configuration.GetConnectionString("DB");
builder.Services.AddDbContext<AppDbContext>(o => o.UseSqlServer(connection));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IFileRepository, FileRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalException>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
