using AuthService;
using DAL;
using DAL.Repositories;
using DAL.Interfaces;
using JwtProvider;
using WebApi.Extencions;
using MyEntitiesService;
using AdminService;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var services = builder.Services;
var configuration = builder.Configuration;

services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));
services.AddDbContext<AppDbContext>();

services.AddIdentity();
services.AddJwtAuthentication(configuration["JwtOptions:SecretKey"]);


services.AddScoped<IAdminService,AdminService.AdminService>();
services.AddScoped<IAuthService, AuthService.AuthService>();
services.AddScoped<IJwtProvider, JwtProvider.JwtProvider>();
services.AddScoped<IMyEntityRepository, MyEntityRepository>();
services.AddScoped<IMyEntityService,MyEntitiesService.MyEntitiesService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
