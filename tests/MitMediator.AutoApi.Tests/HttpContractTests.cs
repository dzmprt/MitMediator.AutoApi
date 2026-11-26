using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using MitMediator.AutoApi.Abstractions;
using MitMediator.AutoApi.Abstractions.Attributes;

namespace MitMediator.AutoApi.Tests;

public class HttpContractTests
{
    [Fact]
    public async Task Get_PreservesQueryValuesThroughHttpMediator()
    {
        await using var app = await StartAppAsync();
        using var services = CreateClientServices(app);
        var mediator = new HttpMediator.HttpMediator(services, "api");
        var request = new GetContractValuesQuery
        {
            Amount = 12.34m,
            Enabled = false,
            Count = 0,
            Key = "ordinary",
            Filter = new() { Label = "nested" }
        };

        var response = await mediator.SendAsync<GetContractValuesQuery, ContractValues>(request, default);

        Assert.Equal(new ContractValues(12.34m, false, 0, "ordinary", "nested"), response);

        using var client = app.GetTestClient();
        var lowerCase = await client.GetFromJsonAsync<ContractValues>(
            "/api/v1/contract-values?amount=12.34&filter.label=lowercase");
        Assert.Equal(new ContractValues(12.34m, null, null, null, "lowercase"), lowerCase);
    }

    [Fact]
    public async Task Multipart_BindsFileAndDtoFieldsThroughHttpMediator()
    {
        await using var app = await StartAppAsync();
        using var services = CreateClientServices(app);
        var mediator = new HttpMediator.HttpMediator(services, "api");
        var request = new ImportContractFileCommand
        {
            Key = 42,
            Label = "upload",
            Enabled = false,
            Filter = new() { Label = "nested" },
            Numbers = [0, 7]
        };
        using var upload = new MemoryStream("contents"u8.ToArray());
        request.SetFile(upload, "sample.txt");

        var response = await mediator.SendAsync<ImportContractFileCommand, ContractFile>(request, default);

        Assert.Equal(42, response.Key);
        Assert.Equal("upload", response.Label);
        Assert.False(response.Enabled);
        Assert.Equal("nested", response.NestedLabel);
        Assert.Equal([0, 7], response.Numbers);
        Assert.Equal("sample.txt", response.FileName);
        Assert.Equal("contents", response.Contents);
    }

    [Fact]
    public async Task OpenApi_ExcludesRouteKeyFromActualJsonBodySchema()
    {
        await using var app = await StartAppAsync();
        using var client = app.GetTestClient();
        using var document = JsonDocument.Parse(await client.GetStringAsync("/openapi/v1.json"));
        var root = document.RootElement;
        var operation = root.GetProperty("paths").GetProperty("/api/v1/contract-values/{key}").GetProperty("put");
        var parameter = Assert.Single(operation.GetProperty("parameters").EnumerateArray());
        Assert.Equal("key", parameter.GetProperty("name").GetString());
        Assert.Equal("path", parameter.GetProperty("in").GetString());
        var bodySchema = operation.GetProperty("requestBody").GetProperty("content")
            .GetProperty("application/json").GetProperty("schema");
        var property = Assert.Single(ResolveSchema(root, bodySchema).GetProperty("properties").EnumerateObject());
        Assert.Equal("note", property.Name);

        var responseSchema = operation.GetProperty("responses").GetProperty("200").GetProperty("content")
            .GetProperty("application/json").GetProperty("schema");
        Assert.True(ResolveSchema(root, responseSchema).GetProperty("properties").TryGetProperty("key", out _));
    }

    private static JsonElement ResolveSchema(JsonElement document, JsonElement schema)
    {
        if (schema.TryGetProperty("$ref", out var reference))
        {
            var schemaName = reference.GetString()!.Split('/')[^1];
            return document.GetProperty("components").GetProperty("schemas").GetProperty(schemaName);
        }

        return schema;
    }

    private static async Task<WebApplication> StartAppAsync()
    {
        var builder = WebApplication.CreateBuilder();
        builder.WebHost.UseTestServer();
        builder.Services.AddOpenApi();
        builder.Services.AddOnlyMitMediator();
        builder.Services.AddTransient<IRequestHandler<GetContractValuesQuery, ContractValues>, GetValuesHandler>();
        builder.Services.AddTransient<IRequestHandler<ImportContractFileCommand, ContractFile>, ImportFileHandler>();
        builder.Services.AddTransient<IRequestHandler<UpdateContractValuesCommand, UpdateContractValuesCommand>, UpdateValuesHandler>();
        var app = builder.Build();
        app.UseAutoApi("api", [typeof(GetContractValuesQuery).Assembly]);
        app.MapOpenApi();
        await app.StartAsync();
        return app;
    }

    private static ServiceProvider CreateClientServices(WebApplication app)
    {
        var services = new ServiceCollection();
        services.AddHttpClient(string.Empty, client => client.BaseAddress = new Uri("http://localhost"))
            .ConfigurePrimaryHttpMessageHandler(() => app.GetTestServer().CreateHandler());
        return services.BuildServiceProvider();
    }

    private sealed class GetValuesHandler : IRequestHandler<GetContractValuesQuery, ContractValues>
    {
        public ValueTask<ContractValues> HandleAsync(GetContractValuesQuery request, CancellationToken cancellationToken) =>
            ValueTask.FromResult(new ContractValues(
                request.Amount, request.Enabled, request.Count, request.Key, request.Filter?.Label));
    }

    private sealed class ImportFileHandler : IRequestHandler<ImportContractFileCommand, ContractFile>
    {
        public async ValueTask<ContractFile> HandleAsync(
            ImportContractFileCommand request, CancellationToken cancellationToken) =>
            new(request.Key, request.Label, request.Enabled, request.Filter?.Label, request.Numbers,
                request.GetFileName(), Encoding.UTF8.GetString(await request.ReadToEndAsync(cancellationToken)));
    }

    private sealed class UpdateValuesHandler : IRequestHandler<UpdateContractValuesCommand, UpdateContractValuesCommand>
    {
        public ValueTask<UpdateContractValuesCommand> HandleAsync(
            UpdateContractValuesCommand request, CancellationToken cancellationToken) =>
            ValueTask.FromResult(request);
    }
}

[Pattern("api/v1/contract-values")]
public sealed class GetContractValuesQuery : IRequest<ContractValues>
{
    public decimal Amount { get; init; }
    public bool? Enabled { get; init; }
    public int? Count { get; init; }
    public string? Key { get; init; }
    public ContractFilter? Filter { get; init; }
}

public sealed class ContractFilter
{
    public string? Label { get; init; }
}

public sealed record ContractValues(decimal Amount, bool? Enabled, int? Count, string? Key, string? Label);

[Pattern("api/v1/contract-files/{key}")]
[DisableAntiforgery]
public sealed class ImportContractFileCommand : FileRequest, IKeyRequest<int>, IRequest<ContractFile>
{
    public int Key { get; init; }
    public string? Label { get; init; }
    public bool? Enabled { get; init; }
    public ContractFilter? Filter { get; init; }
    public int[]? Numbers { get; init; }
}

public sealed record ContractFile(
    int Key, string? Label, bool? Enabled, string? NestedLabel, int[]? Numbers, string FileName, string Contents);

[Pattern("api/v1/contract-values/{key}")]
public sealed class UpdateContractValuesCommand : IKeyRequest<int>, IRequest<UpdateContractValuesCommand>
{
    public int Key { get; init; }
    public string? Note { get; init; }
}
