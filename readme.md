# MitMediator.AutoApi

Attribute-driven Minimal API registration for MitMediator

## 🚀 Purpose

To automate the registration of HTTP endpoints from attribute-decorated request types

## 🧩 How It Works

1. Scans all loaded assemblies for `IRequest` types decorated with action attributes (e.g. `[Create]`, `[GetByKey]`, etc.).
2. For each matching type, dynamically builds and registers endpoints.
3. Routes are mapped using `MapPost`, `MapGet`, `MapPut`, and `MapDelete`, invoking `IMediator.SendAsync()` under the hood.

## 🔖 Supported Attributes

| Attribute     | HTTP Method | Default Route    |
|---------------|-------------|------------------|
| `Create`      | POST        | `/v1/tag`        |
| `Post`        | POST        | `/v1/tag`        |
| `Update`      | PUT         | `/v1/tag`        |
| `UpdateByKey` | PUT         | `/v1/tag/{key}`  |
| `Delete`      | DELETE      | `/v1/tag`        |
| `DeleteByKey` | DELETE      | `/v1/tag/{key}`  |
| `Get`         | GET         | `/v1/tag`        |
| `GetByKey`    | GET         | `/v1/tag/{key}`  |

## ⚙️ Setup

```csharp
var builder = WebApplication.CreateBuilder(args);
// Register handlers and IMediator.
builder.Services.AddMitMediator(); 

var app = builder.Build();
// Automatically maps endpoints from attributes
app.UseAutoApi(); 

app.Run();
```

## 🧪 Example Command

```csharp
// Crate POST /v1/users, 
// return result with http status code 201
[Create("uses", "v1")]
public class CreateUserCommand : IRequest<UserDto>
{
    public string Name { get; set; }
    public int Age { get; set; }
}
```

## 💡 Features

* Declarative routing via attributes
* Automatic tag and version grouping
* Built-in handling of `Unit` results (no payload responses)
* Key binding support for endpoints requiring `{key}`.
* No source generators required — all mapping is runtime-based and cached.
