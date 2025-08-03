[![Build and Test](https://github.com/dzmprt/MitMediator/actions/workflows/dotnet.yml/badge.svg)](https://github.com/dzmprt/MitMediator/actions/workflows/dotnet.yml)
![NuGet](https://img.shields.io/nuget/v/MitMediator.AutoApi)
![.NET 7.0](https://img.shields.io/badge/Version-.NET%207.0-informational?style=flat&logo=dotnet)
![License](https://img.shields.io/github/license/dzmprt/MitMediator.AutoApi)
# MitMediator.AutoApi

## Minimal API registration for MitMediator

### 🔗 Extension for [MitMediator](https://github.com/dzmprt/MitMediator)

## 🚀 Installation

### 1. Add package
```bash
# for ASP.NET API projects
dotnet add package MitMediator.AutoApi -v 7.0.0-alfa-7

# for application layer
dotnet add package MitMediator.AutoApi.Abstractions -v 7.0.0-alfa-7

# for client application (MAUI, Blazor, UWP, etc.)
dotnet add package MitMediator.AutoApi.HttpMediator -v 7.0.0-alfa-7
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

### `GET` endpoint 
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

### `GET` endpoint with key in url
```csharp
// Use IKeyRequest<> to set and get key from ur.
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
    
    public int GetKey() => BookId;
}
```

### `GET` endpoint with suffix
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

### `POST` endpoint with 201 response
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

### `PUT` endpoint with key in url
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
    
    public int GetKey() => BookId;
}
```

### `DELETE` endpoint with key in url
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
    
    public int GetKey() => BookId;
}
```

### `GET` endpoint text file ("application/octet-stream")
```csharp
// Get - GET http method
// Book - main tag (books in url)
// Text - action suffix
// Api URL: GET /books/text/123
public class GetBookTextQuery: IRequest<byte[]>, IKeyRequest<int>
{
    internal int BookId { get; private set; }
    
    public void SetKey(int key)
    {
        BookId = key;
    }
    
    public int GetKey() => BookId;
}
```

### `GET` endpoint png file ("image/png")
```csharp
// Get - GET http method
// Book - main tag (books in url)
// Cover - action suffix
// Api URL: GET /books/сover/123
[AutoApi(customResponseContentType:"image/png")]
public class GetBookCoverQuery: IRequest<byte[]>, IKeyRequest<int>
{
    internal int BookId { get; private set; }
    
    public void SetKey(int key)
    {
        BookId = key;
    }
    
    public int GetKey() => BookId;
}
```

### Change default mapping

Use the `[AutoApi]` attribute for the request type to change default mapping

### `GET` endpoint with version, custom tag and custom suffix
```csharp
// Get - http method
// Books - main tag (books in url)
// API version: "v2"
// Api URL: GET v2/my-books/favorite?limit=1&offset=1&freeText=clara
[AutoApi("my-books", "v2", PatternSuffix = "favorite")]
public struct GetBooksQuery : IRequest<Book[]>
{
    public int? Limit { get; init; }
    
    public int? Offset { get; init; }
    
    public string? FreeText { get; init; }
}
```
### `GET` endpoint with version, custom tag, URL pattern and specified HTTP method
```csharp
// Auto-generated DELETE endpoint.
// Base tag: "books"
// API version: "v3"
// Custom URL pattern overrides base route: DELETE with-keys/{key1}/field/{key2}
[AutoApi("books", customPattern: "with-keys/{key1}/field/{key2}", version: "v3", httpMethodType:HttpMethodType.Delete)]
public class DoSomeWithBookAndDeleteCommand : IRequest<Book[]>, IKeyRequest<int, Guid>
{
    internal int BookId { get; private set; }
    
    internal Guid GuidId { get; private set; }

    public void SetKey1(int key)
    {
        BookId = key;
    }
    
    public int GetKey1() => BookId;
    
    public void SetKey2(Guid key)
    {
        GuidId = key;
    }
    
    public Guid GetKey2() => GuidId;
}
```

### 🎯 Use auto client: HttpMediator

You can reuse your `IRequest` types to seamlessly send HTTP requests to a server-side API using `HttpMediator`

`HttpMediator` supports `IPipelineBehavior<TRequest, TResponse>` for middleware-like extensibility, and `IHttpHeaderInjector<TRequest, TResponse>` for injecting custom headers per request

### 🔧 Sample usage:

```csharp
var mediator = new HttpMediator(serviceProvider, baseUrl: "https://api.example.com");
var response = await mediator.SendAsync<MyRequest, MyResponse>(new MyRequest(), cancellationToken);
```

### 🔧 More
```csharp
var baseApiUrl = "api";
var httpClientName = "baseHttpClient";
var serviceCollection = new ServiceCollection();
serviceCollection.AddHttpClient(httpClientName, client => { client.BaseAddress = new Uri("https://localhost:7127/"); });
serviceCollection.AddScoped(typeof(IHttpHeaderInjector<,>), typeof(AuthorizationHeaderInjection<,>));
serviceCollection.AddScoped<IHttpMediator, HttpMediator>(c => new HttpMediator(c, baseApiUrl, httpClientName));

var provider = serviceCollection.BuildServiceProvider();
var httpMediator = provider.GetRequiredService<IHttpMediator>();

var query = new GetBookQuery();
query.SetKey(12);
var data = await httpMediator.SendAsync<GetBookQuery, Book>(query, CancellationToken.None);

public class AuthorizationHeaderInjection<TRequest, TResponse> : IHttpHeaderInjector<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public ValueTask<(string, string)?> GetHeadersAsync(CancellationToken cancellationToken)
    {
        var result = ("Authorization", "Bearer token");
        return ValueTask.FromResult<(string, string)?>(result);
    }
}
```

### 📁 See [samples](./samples)

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
| `fetch`                 | GET                 |
| `update`                | PUT                 |
| `change`                | PUT                 |
| `edit`                  | PUT                 |
| `modify`                | PUT                 |
| `put`                   | PUT                 |
| `post`                  | POST                |
| `import`                | POST                |
| `upload`                | POST                |
| `add`                   | POST (201 response) |
| `create`                | POST (201 response) |
| `delete`                | DELETE              |
| `remove`                | DELETE              |
| `drop`                  | DELETE              |

The first word after the method will be the main tag. Second and other - suffix. For example:

`GetBookCountQuery` - Get - http method GET, Book - main tag (books in url), Count - suffix (/count in url)

`RemoveBookWithAuthorCommand` - Remove - http method DELETE, Book - main tag (books in url), WithAuthor - suffix (/with-author in url)

Words `command`, `query`, and `request` in the end of request type name will be deleted from url

## 📄 File response (`byte[]` and `FileResponse`)

For requests returning `byte[]` (via `IRequest<byte[]>`), the response uses the "application/octet-stream" content type by default.
To specify a download file name, use the FileResponse class. Use `[AutoApi(customResponseContentType:"image/png")]` attribute for custom content type

## 💡 Features

* Declarative routing via attributes
* Automatic tag and version grouping
* Custom route patterns with up to 7 composite keys
* Built-in handling of `Unit` results (no payload responses)
* Key binding via IKeyRequest<TKey1, TKey2, ...> (pattern `{key1}/{key2}/.../{key7}`)
* Supports custom patterns (`custom-route/my-route`, `with-keys/{key1}/{key2}/field/{key3}`)
* Auto client `HttpMediator` for clients applications
* File response (`IRequest<byte[]>` or `IRequest<FileResponse>`)

## 📜 License

MIT
