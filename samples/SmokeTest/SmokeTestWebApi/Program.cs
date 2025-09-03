using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using MitMediator.AutoApi;
using SmokeTest.Application.UseCase.Test.Queries.GetEmpty;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMitMediator();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Test api", Version = "v1", Description = "Test API project for MitMediator.AutoApi" });
    options.SwaggerDoc("v2", new OpenApiInfo { Title = "Test api v2", Version = "v2", Description = "Test API project for MitMediator.AutoApi" });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithExposedHeaders("Content-Disposition")
            .WithExposedHeaders("X-Total-Count");
    });
});

var app = builder.Build();
app.UseCors();
app.UseAutoApi("api", [typeof(GetEmptyTestQuery).Assembly]);

// Configure the HTTP request pipeline.
app.UseSwagger(c => { c.RouteTemplate = "swagger/{documentname}/swagger.json"; })
    .UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.SwaggerEndpoint("/swagger/v2/swagger.json", "v2");
        options.RoutePrefix = "swagger";
    });

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();