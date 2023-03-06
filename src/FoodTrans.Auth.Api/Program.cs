using Api.Services;
using Application;
using Application.Contracts;
using FoodTrans.Auth.Api.Common.Errors;
using Infrastructure;
using Infrastructure.Persistance;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddApplication()
                    .AddInfrastructure(builder.Configuration);
    builder.Services.AddSingleton<ProblemDetailsFactory, ApiProblemDetailsFactory>();
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();
{
    app.UseExceptionHandler("/error");
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseInfrastructure();

    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}