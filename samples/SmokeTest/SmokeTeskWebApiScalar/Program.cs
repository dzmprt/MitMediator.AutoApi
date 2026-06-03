using MitMediator.AutoApi;
using Scalar.AspNetCore;
using SmokeTest.Application.UseCase.Test.Queries.GetEmpty;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddMitMediator();
builder.Services.AddOpenApi("v1");
builder.Services.AddOpenApi("v2");

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

app.MapOpenApi();
app.MapScalarApiReference(options =>
{
    options.AddDocument("v1", "v1 API", "/openapi/v1.json", isDefault: true)
           .AddDocument("v2", "v2 API", "/openapi/v2.json");
});
app.UseHttpsRedirection();

app.Run();