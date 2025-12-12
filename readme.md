[![Build and Test](https://github.com/dzmprt/MitMediator/actions/workflows/dotnet.yml/badge.svg)](https://github.com/dzmprt/MitMediator/actions/workflows/dotnet.yml)
![NuGet](https://img.shields.io/nuget/v/MitMediator.AutoApi)
![.NET 10.0](https://img.shields.io/badge/Version-.NET%2010-informational?style=flat&logo=dotnet)
![License](https://img.shields.io/github/license/dzmprt/MitMediator.AutoApi)

# MitMediator.AutoApi

## An extension for [MitMediator](https://github.com/dzmprt/MitMediator) that automatically registers API endpoints and generates strongly-typed HTTP clients from mediator request types, with customizable behavior driven by attributes

- [🚀 Installation](#-Installation)
- [How It Works](#how-it-works)
- [Examples](#examples)
- [Change default mapping](#change-default-mapping)
- [Upload and download files](#upload-and-download-files)
- [X-Total-Count Header](#x-total-count-header)
- [Location header](#location-header)
- [Auto client HttpMediator](#auto-client-httpmediator)
- [Samples](./samples)
- [License](LICENSE)

## 🚀 Installation

### 1. Add package

```bash
# for ASP.NET API projects
dotnet add package MitMediator.AutoApi -v 10.0.0-alfa-3

# for application layer
dotnet add package MitMediator.AutoApi.Abstractions -v 10.0.0-alfa-3

# for client application (MAUI, Blazor, UWP, etc.)
dotnet add package MitMediator.AutoApi.HttpMediator -v 10.0.0-alfa-3
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

> To use base path "**api**" and select assemblies to scan: `app.UseAutoApi("api", new []{typeof(GetQuery).Assembly});`

### 3. Done! All public requests have endpoints

## How It Works

1. Scans all loaded assemblies for `IRequest` types
2. Dynamically generates and registers endpoints for each match
3. Maps routes using `MapPost`, `MapGet`, `MapPut`, and `MapDelete`, internally calling `IMediator.SendAsync`

### Transformation Logic

HTTP Method is inferred from the leading verb in the type name:

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

Resource Name (main tag) is derived from the first noun after the verb, and pluralized:

- Book → /books 
- Author → /authors

Route suffix includes any remaining parts of the type name, converted to lowercase and kebab-case:

- Count → /count
- WithAuthor → /with-author

> Custom suffix will be added as is

Suffixes `Command`, `Query`, and `Request` are automatically removed from the end

Version prefix (v1, v2, etc.) is prepended to the route as part of the base path

> Default version is `v1`

## Examples

| Request name                | Endpoint                       |
|-----------------------------|--------------------------------|
| GetBookCountQuery           | `GET /v1/books/count`          |
| CreateBookCommand           | `POST /v1/books`                |
| RemoveBookWithAuthorCommand | `DELETE /v1/books/with-author` |
| UpdateAuthorBioRequest      | `PUT /v1/authors/bio`          |

### `GET` endpoint with query params

```csharp
// Api URL: GET /v1/books?limit=1&offset=1&freeText=clara
public struct GetBooksQuery : IRequest<Book[]>
{
    public int? Limit { get; init; }
    
    public int? Offset { get; init; }
    
    public string? FreeText { get; init; }
}
```

### `POST` endpoint with 201 response

```csharp
// Api URL: POST /v1/books
public class CreateBookCommand : IRequest<Book>
{
    public string Title { get; init; }
    
    public int AuthorId { get; init; }

    public string GenreName { get; init; }
}
```

If you need to use a route parameter, inherit from one of the classes `KeyRequest<TKey>`
or implement one of the interfaces `IKeyRequest<TKey>`, `IKeyRequest<TKey1,TKey2>`, etc.
(up to 7 keys).

### `GET` endpoint with suffix and key in url

```csharp
// URL: GET /v1/books/123/cover
public class GetBookCoverQuery : KeyRequest<int>, IRequest<Book>;
```
> Use interface `IKeyRequest<int>` for structs or when you can't inherit KeyRequest:
> ```csharp 
> // URL: GET /v1/books/123/cover
> public struct GetBookCoverQuery : IKeyRequest<int>, IRequest<Book>
> {
>     internal int BookId { get; private set; }
>
>     public void SetKey(int key)
>     {
>         BookId = key;
>     }
>     
>     public int GetKey() => BookId;
> }
> ```

## Change default mapping

Use attributes for the request type to change default mapping

Supported attributes:

| Attribute                      | Template                                       | Result for `GetBookRequest`                               |
|--------------------------------|------------------------------------------------|-----------------------------------------------------------|
| `TagAttribute`                 | `[Tag("CustomTag")]`                           | `GET v1/custom-tag`                                       |
| `VersionAttribute`             | `[Version("v2")]`                              | `GET v2/books`                                            |
| `SuffixAttribute`              | `[Suffix("cover")]`                            | `GET v1/books/cover`                                      |
| `PatternAttribute`             | `[Pattern("api/v3/books/{key1}/page/{key2}")]` | `GET api/v3/books/{key1}/page/{key2}`                     |
| `MethodAttribute`              | `[Method(MethodType.Post)]`                    | `POST v1/books`                                           |
| `DescriptionAttribute`         | `[Description("My custom description")]`       | See description (in xml doc or swagger)                   |
| `DisableAntiforgeryAttribute`  | `[DisableAntiforgery]`                         | Antiforgery protection has been disabled for the endpoint |
| `ResponseContentTypeAttribute` | `[ResponseContentType("image/png")]`           | Result will have selected content type                    |
| `IgnoreRequestAttribute`       | `[IgnoreRequest]`                              | Request will be ignored 🚫                                |

When `[PatternAttribute]` is applied, it overrides the entire route. The base route, version attribute, and tag attribute will be ignored

### Examples

### `GET` endpoint with custom tag and custom suffix

```csharp
// URL: GET /v1/my-books/favorite?limit=1&offset=1&freeText=clara
[Tag("MyBooks")]
[Suffix("favorite")]
public struct GetBooksQuery : IRequest<Book[]>
{
    public int? Limit { get; init; }
    
    public int? Offset { get; init; }
    
    public string? FreeText { get; init; }
}
```

### `GET` endpoint with version, custom tag, URL pattern and specified HTTP method

```csharp
// Custom URL pattern overrides base route
// URL: DELETE /with-keys/{key1}/field/{key2}
[Tag("books")]
[Pattern("with-keys/{key1}/field/{key2}")]
[Version("v3")]
[Method(MethodType.Delete)]
public class DoSomeWithBookAndDeleteCommand : KeyRequest<int, Guid>, IRequest<Book[]>;
```

### `GET` endpoint png file ("image/png")

```csharp
// Api URL: GET /v1/books/123/сover
[ResponseContentType("image/png")]
public class GetBookCoverQuery: KeyRequest<int>, IRequest<byte[]>;
```

## Upload and download files

For requests implementing `IRequest<byte[]>`/`IRequest<Stream>`, or  `IRequest<FileResponse>`/
`IRequest<FileStreamResponse>`, the response will use `"application/octet-stream"` by default. To specify a download
file name, use the `FileResponse` or `FileStreamResponse` response.
Use `[ResponseContentType("image/png")]` attribute for custom content type

When a request implements `IFileRequest` or inherits from `FileRequest`, it will be bound using `[FromForm]`.
File parameters fron `IFileRequest` will be automatically inferred via `IFromFile`. Apply `[DisableAntiforgery]` to skip 
CSRF protection for file upload endpoints or non-browser clients

### Examples

### `GET` endpoint text file ("application/octet-stream")

```csharp
// Api URL: GET /v1/books/123/text
public class GetBookTextQuery: KeyRequest<int>, IRequest<byte[]>;
```

### `POST` upload and download file with file name

```csharp
// Api URL: POST /v1/documents/word
[DisableAntiforgery]
public class ImportDocumentWordCommand : FileRequest, IRequest<FileResponse>;
}
```

## X-Total-Count Header

To include the `X-Total-Count` header in the HTTP response, implement the `ITotalCount` interface in your response type.
This is useful for paginated endpoints or any scenario where the client needs to know the total number of items
available.

```csharp
public class GetBooksResponse : ITotalCount
{
    public Book[] Items { get; init; }

    private int _totalCount;
    
    public int GetTotalCount() => _totalCount;

    public void SetTotalCount(int totalCount)
    {
        _totalCount = totalCount;
    }
}
```

When this interface is implemented, MitMediator.AutoApi will automatically include the `X-Total-Count` header in the
response, reflecting the value returned by `GetTotalCount()`

## Location header

To insert the correct ID into the Location header of a 201 Created response, implement the `IResourceKey` interface in
your response type:

```csharp
// Api URL: POST /books
public class CreateBookCommand : IRequest<CreatedBookResponse>
{
    public string Title { get; init; }
}

// Location Header: /books/{BookId}
public class CreatedBookResponse : IResourceKey
{
    public int BookId { get; private set; }
    
    public string Title { get; private set; }
    
    public string GetResourceKey() => BookId.ToString();
}
```

If you do not implement the `IResourceKey` interface, the Location header will default to the format `/books/{key}`,
where `{key}` is a placeholder string

# Auto client HttpMediator

You can reuse your `IRequest` types to seamlessly send HTTP requests to a server-side API using `HttpMediator`

`HttpMediator` supports `IClientPipelineBehavior<TRequest, TResponse>` for middleware-like extensibility, and
`IHttpHeaderInjector<TRequest, TResponse>` for injecting custom headers per request

### Sample usage:

```csharp
var mediator = new HttpMediator(serviceProvider, baseUrl: "https://api.example.com");
var response = await mediator.SendAsync<MyRequest, MyResponse>(new MyRequest(), cancellationToken);
```

### More

```csharp
var baseApiUrl = "api";
var httpClientName = "baseHttpClient";
var serviceCollection = new ServiceCollection();
serviceCollection.AddHttpClient(httpClientName, client => { client.BaseAddress = new Uri("https://localhost:7127/"); });
serviceCollection.AddScoped(typeof(IHttpHeaderInjector<,>), typeof(AuthorizationHeaderInjection<,>));
serviceCollection.AddScoped<IClientMediator, HttpMediator>(c => new HttpMediator(c, baseApiUrl, httpClientName));

var provider = serviceCollection.BuildServiceProvider();
var httpMediator = provider.GetRequiredService<IClientMediator>();

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

## See [samples](./samples)

## MIT [License](LICENSE)
