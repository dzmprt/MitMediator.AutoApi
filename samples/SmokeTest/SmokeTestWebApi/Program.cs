using MitMediator.AutoApi;
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

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/openapi/v1.json", "v1");
    options.SwaggerEndpoint("/openapi/v2.json", "v2");
});
app.UseHttpsRedirection();

app.Run();