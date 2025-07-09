[![Build and Test](https://github.com/dzmprt/MitMediator/actions/workflows/dotnet.yml/badge.svg)](https://github.com/dzmprt/MitMediator/actions/workflows/dotnet.yml)
![NuGet](https://img.shields.io/nuget/v/MitMediator.AutoApi)
![.NET 7.0](https://img.shields.io/badge/Version-.NET%207.0-informational?style=flat&logo=dotnet)
![License](https://img.shields.io/github/license/dzmprt/MitMediator.AutoApi)
# MitMediator.AutoApi

## Attribute-driven Minimal API registration for MitMediator
### 🔗 Extension for [MitMediator](https://github.com/dzmprt/MitMediator)

## 🚀 Installation

### 1. Add package
```bash
# for ASP.NET API projects
dotnet add package MitMediator.AutoApi -v 7.0.0-alfa-3

# for application layer
dotnet add package MitMediator.AutoApi.Abstractions -v 7.0.0-alfa-3
```
### 2. Use extension for IEndpointRouteBuilder

```csharp
var builder = WebApplication.CreateBuilder(args);
// Register handlers and IMediator.
builder.Services.AddMitMediator(); 

var app = builder.Build();

// Automatically maps endpoints based on IRequest
app.UseAutoApi(); 

app.Run();
```
To use base path "api" and select assemblies with requests:
```csharp
app.UseAutoApi("api", new []{typeof(GetQuery).Assembly});
```

### 3. Done! All public requests have endpoints

## 🧪 Examples

### `GET` method 
```csharp
// Get - http method
// Books - main tag
// Api URL: GET /books?limit=1&offset=1&freeText=clara
public struct GetBooksQuery : IRequest<Book[]>
{
    public int? Limit { get; init; }
    
    public int? Offset { get; init; }
    
    public string? FreeText { get; init; }
}
```

### `GET` method with key in url
```csharp
// Use IKeyRequest<> to get get from url.
// Get - http method
// Book - main tag (books in url)
// Api URL: GET /books/123
public struct GetBookQuery : IRequest<Book>, IKeyRequest<int>
{
    internal int BookId { get; private set; }

    public void SetKey(int key)
    {
        BookId = key;
    }
}
```

### `GET` method with suffix
```csharp
// Get - http method
// Books - main tag
// Count - action suffix
// Api URL: GET /books/count?freeText=clara
public struct GetBooksCountQuery : IRequest<int>
{
    public string? FreeText { get; init; }
}
```

### `POST` method with 201 response
```csharp
// Create - POST http method, return 201
// Book - main tag (books in url)
// Api URL: POST /books
public class CreateBookCommand : IRequest<Book>
{
    public string Title { get; init; }
    
    public int AuthorId { get; init; }

    public string GenreName { get; init; }
}
```

### `PUT` method with key in url
```csharp
// Update - PUT http method
// Book - main tag (books in url)
// Api URL: PUT /books/123
public class UpdateBookCommand : IRequest<Book>, IKeyRequest<int>
{
    internal int BookId { get; private set; }
    
    public string Title { get; init; }
    
    public int AuthorId { get; init; }

    public string GenreName { get; init; }

    public void SetKey(int key)
    {
        BookId = key;
    }
}
```

### `DELETE` method with key in url
```csharp
// Delete - DELETE http method
// Book - main tag (books in url)
// Api URL: DELETE /books/123
public struct DeleteBookCommand : IRequest, IKeyRequest<int>
{
    internal int BookId { get; private set; }

    public void SetKey(int key)
    {
        BookId = key;
    }
}
```

### Change default mapping

Use the `[AutoApi]` attribute for the request type to change default mapping

### `GET` method with version and custom tag
```csharp
// Get - http method
// books - main tag
// v2 - api version
// Api URL: GET v2/new-books?limit=1&offset=1&freeText=clara
[AutoApi("new-books", "v2")]
public struct GetBooksQuery : IRequest<Book[]>
{
    public int? Limit { get; init; }
    
    public int? Offset { get; init; }
    
    public string? FreeText { get; init; }
}
```
### `GET` method with version, custom pattern and selected http method
```csharp
[AutoApi(customPattern: "with-keys/{key1}/{key2}", version: "v3", httpMethodType:HttpMethodType.Delete)]
public class DoSomeWithBookAndDeleteCommand : IRequest<Book[]>, IKeyRequest<int, Guid>
{
    internal int BookId { get; private set; }
    
    internal Guid GuidId { get; private set; }

    public void SetKey1(int key)
    {
        BookId = key;
    }
    
    public void SetKey2(Guid key)
    {
        GuidId = key;
    }
}
```

### 📁 See [samples](./samples) for usage examples

## ⚙️ How It Works

1. Scans all loaded assemblies for `IRequest` types
2. Dynamically generates and registers endpoints for each match
3. Maps routes using `MapPost`, `MapGet`, `MapPut`, and `MapDelete`, internally calling `IMediator.SendAsync()`

If you need to change the generated method, use the `[AutoApi]` attribute for the request type

The HTTP method type is determined automatically according to the request name:

| Request name start with | HTTP Method         |
|-------------------------|---------------------|
| `get`                   | GET                 |
| `load`                  | GET                 |
| `download`              | GET                 |
| `update`                | PUT                 |
| `change`                | PUT                 |
| `edit`                  | PUT                 |
| `modify`                | PUT                 |
| `put`                   | PUT                 |
| `post`                  | POST                |
| `add`                   | POST (201 response) |
| `create`                | POST (201 response) |
| `upload`                | POST (201 response) |
| `delete`                | DELETE              |
| `remove`                | DELETE              |
| `drop`                  | DELETE              |

The first word after the method will be the main tag. Second and other - suffix. For example:

`GetBookCountQuery` - Get - http method GET, Book - main tag (books in url), Count - suffix (/count in url)

`RemoveBookWithAuthorCommand` - Remove - http method DELETE, Book - main tag (books in url), WithAuthor - suffix (/with-author in url)

Words `command`, `query`, and `request` in the end of request type name will be deleted from url.

## 💡 Features

* Declarative routing via attributes
* Automatic tag and version grouping
* Custom route patterns with up to 7 composite keys
* Built-in handling of `Unit` results (no payload responses)
* Key binding via IKeyRequest<TKey1, TKey2, ...> (pattern `{key1}/{key2}/.../{key7}`)
* Supports custom patterns (`custom-route/my-route`, `with-keys/{key1}/{key2}/field/{key3}`)
