using System.Reflection;
using LiteTestWebApi.UseCase.Test.Queries.GetEmpty;
using Microsoft.OpenApi.Models;
using MitMediator.AutoApi;

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

var app = builder.Build();
app.UseAutoApi("api", new []{typeof(GetEmptyTestQuery).Assembly});

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