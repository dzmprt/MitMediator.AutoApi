[![Build and Test](https://github.com/dzmprt/MitMediator/actions/workflows/dotnet.yml/badge.svg)](https://github.com/dzmprt/MitMediator/actions/workflows/dotnet.yml)
![NuGet](https://img.shields.io/nuget/v/MitMediator.AutoApi)
![License](https://img.shields.io/github/license/dzmprt/MitMediator.AutoApi)
# MitMediator.AutoApi

## Attribute-driven Minimal API registration for MitMediator
### 🔗 Extension for [MitMediator](https://github.com/dzmprt/MitMediator)

## 🚀 Installation

### For ASP.NET API projects
Install package to enable attribute-based registration:
```bash
dotnet add package MitMediator.AutoApi -v 7.0.0-alfa
```

### For application layer projects
Install the abstractions package to define attributes and interfaces:
```bash
dotnet add package MitMediator.AutoApi.Abstractions -v 7.0.0-alfa
```

## ⚙️ Setup

```csharp
var builder = WebApplication.CreateBuilder(args);
// Register handlers and IMediator.
builder.Services.AddMitMediator(); 

var app = builder.Build();

// Automatically maps endpoints based on IRequest attributes with one of attributes
app.UseAutoApi(); 

app.Run();
```

## 🧩 How It Works

1. Scans all loaded assemblies for `IRequest` types decorated with action attributes (e.g. `[Create]`, `[GetByKey]`, etc.)
2. Dynamically generates and registers endpoints for each match
3. Maps routes using `MapPost`, `MapGet`, `MapPut`, and `MapDelete`, internally calling `IMediator.SendAsync()`

## 🔖 Supported Attributes

| Attribute     | HTTP Method | Default Route    | Response status code |
|---------------|-------------|------------------|----------------------| 
| `Create`      | POST        | `/v1/tag`        | 201                  |
| `CreateByKey` | POST        | `/v1/tag/{key}`  | 201                  |
| `Post`        | POST        | `/v1/tag`        | 200                  |
| `PostByKey`   | POST        | `/v1/tag/{key}`  | 200                  |
| `Update`      | PUT         | `/v1/tag`        | 200                  |
| `UpdateByKey` | PUT         | `/v1/tag/{key}`  | 200                  |
| `Delete`      | DELETE      | `/v1/tag`        | 200                  |
| `DeleteByKey` | DELETE      | `/v1/tag/{key}`  | 200                  |
| `Get`         | GET         | `/v1/tag`        | 200                  |
| `GetByKey`    | GET         | `/v1/tag/{key}`  | 200                  |

## 🧪 Example Command

```csharp
// Maps to POST /v1/users with status 201
[Create("uses", "v1")]
public class CreateUserCommand : IRequest<UserDto>
{
    public string Name { get; set; }

    public int Age { get; set; }
}
```

You can override default route generation by providing a custom pattern through the attribute’s customPattern property. This enables fine-grained control over endpoint structure

```csharp
// Maps to DELETE /v2/items/remove/{key1}/{key2} endpoint, 
// return no payload responses with http status code 200
[DeleteByKey("any-tag", customPattern: "v2/items/remove/{key1}/{key2}")]
public class DeleteItemCommand : IRequest, IKeyRequest<Guid, int>
{
    internal Guid GuidId { get; set; }
    
    internal int IntId { get; set; }

    public void SetKey1(Guid id) => GuidId = id;
    
    public void SetKey2(int id) => IntId = id;
}
```

## 💡 Features

* Declarative routing via attributes
* Automatic tag and version grouping
* Custom route patterns with up to 7 composite keys
* Built-in handling of `Unit` results (no payload responses)
* Key binding via IKeyRequest<TKey1, TKey2, ...> (pattern `{key1}/{key2}/.../{key7}`)
* Supports custom patterns (`custom-route/my-route`, `with-keys/{key1}/{key2}/field/{key3}`)
