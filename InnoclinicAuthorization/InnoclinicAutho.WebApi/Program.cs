using FluentValidation;
using InnoclinicAutho.Application.Interfaces;
using InnoclinicAutho.Application.Interfaces.Repositories;
using InnoclinicAutho.Application.UseCases.Behavior;
using InnoclinicAutho.Application.UseCases.Users.Commands.RegisterUser;
using InnoclinicAutho.Application.UseCases.Users.Commands.RegisterUser.Patient;
using InnoclinicAutho.Infrastructure.Caching;
using InnoclinicAutho.Persistence.Extensions;
using InnoclinicAutho.Persistence.Repositories;
using InnoclinicAutho.WebApi.Middleware;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services
    .AddDbContext(builder.Configuration.GetConnectionString("DefaultConnection"))
    .AddJwtAuth(builder.Configuration);

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();

builder.Services.AddScoped<ICacheService, CacheService>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(RegisterPatientCommandHandler)));
builder.Services.AddValidatorsFromAssemblyContaining(typeof(RegisterUserCommandValidator));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

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
