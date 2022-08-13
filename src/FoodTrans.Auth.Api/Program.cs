using Application;
using FoodTrans.Auth.Api.Common.Errors;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddApplication();
    builder.Services.AddSingleton<ProblemDetailsFactory, ApiProblemDetailsFactory>();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}