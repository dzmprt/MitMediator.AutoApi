using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using Microsoft.Extensions.DependencyInjection;
using MitMediator.AutoApi.Abstractions;
using MitMediator.AutoApi.HttpMediator.Extensions;

namespace MitMediator.AutoApi.HttpMediator;

internal sealed class HttpRequestHandler<TRequest, TResponse> : IClientRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private static readonly JsonSerializerOptions RequestSerializerOptions = CreateRequestSerializerOptions();
    private static readonly JsonSerializerOptions SerializerOptions = CreateSerializerOptions();

    private readonly IServiceProvider _serviceProvider;
    
    private readonly string? _baseUrl;

    private readonly HttpClient _httpClient;

    public HttpRequestHandler(IServiceProvider serviceProvider, string? baseUrl, HttpClient httpClient)
    {
        _serviceProvider = serviceProvider;
        _baseUrl = baseUrl;
        _httpClient = httpClient;
    }
    
    public async ValueTask<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken)
    {
        var requestUrl = HttpRequestsHelper.GetUrl(request, _baseUrl);
        var requestInfo = RequestInfoCache.Get(typeof(TRequest));
        using var httpRequest = CreateHttpRequest(request, requestUrl, requestInfo.MethodType);
        var httpHeaderInjectors = _serviceProvider
            .GetServices<IHttpHeaderInjector<TRequest, TResponse>>();
        foreach (var httpHeaderInjector in httpHeaderInjectors)
        {
            var header = await httpHeaderInjector.GetHeadersAsync(cancellationToken);
            if (header.HasValue)
            {
                httpRequest.Headers.Add(header.Value.Item1, header.Value.Item2);
            }
        }

        var response = await _httpClient.SendAsync(
            httpRequest,
            HttpCompletionOption.ResponseHeadersRead,
            cancellationToken);

        try
        {
            var convertedResponse = await ConvertResponseAsync(response, cancellationToken);
            if (typeof(TResponse) == typeof(Stream) || typeof(TResponse) == typeof(FileStreamResponse))
            {
                return convertedResponse.Item1;
            }

            response.Dispose();
            return convertedResponse.Item1;
        }
        catch
        {
            response.Dispose();
            throw;
        }
    }

    private static HttpRequestMessage CreateHttpRequest(
        TRequest request,
        string requestUrl,
        MethodType methodType)
    {
        var method = methodType switch
        {
            MethodType.Post or MethodType.PostCreate => HttpMethod.Post,
            MethodType.Put => HttpMethod.Put,
            MethodType.Delete => HttpMethod.Delete,
            _ => HttpMethod.Get
        };
        var httpRequest = new HttpRequestMessage(method, requestUrl);

        if (method == HttpMethod.Post || method == HttpMethod.Put)
        {
            if (request is IFileRequest fileRequest)
            {
                var form = new MultipartFormDataContent();
                httpRequest.Content = form;
                try
                {
                    form.Add(new StreamContent(fileRequest.GetFileStream()), "formFile", fileRequest.GetFileName());
                    foreach (var field in request.GetFormFields())
                    {
                        form.Add(new StringContent(field.Value), field.Key);
                    }
                }
                catch
                {
                    httpRequest.Dispose();
                    throw;
                }
            }
            else
            {
                httpRequest.Content = new StringContent(
                    JsonSerializer.Serialize(request, RequestSerializerOptions),
                    Encoding.UTF8,
                    "application/json");
            }
        }

        return httpRequest;
    }
    
    private static async ValueTask<(TResponse, HttpResponseHeaders responseHeaders)> ConvertResponseAsync(
        HttpResponseMessage response, CancellationToken cancellationToken)
    {
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync(cancellationToken);
            throw new HttpRequestException(string.IsNullOrWhiteSpace(error) ? response.ReasonPhrase : error, null, response.StatusCode);
        }
        
        if (typeof(TResponse) == typeof(byte[]))
        {
            var result = await response.Content.ReadAsByteArrayAsync(cancellationToken);
            return ((TResponse)(object)result, response.Headers);
        }
        if (typeof(TResponse) == typeof(FileResponse))
        {
            var data = await response.Content.ReadAsByteArrayAsync(cancellationToken);
            var fileName = response.Content.Headers.ContentDisposition?.FileName ?? string.Empty;
            var result = new FileResponse(data, fileName);
            return ((TResponse)(object)result, response.Headers);
        }
        if (typeof(TResponse) == typeof(Stream))
        {
            var result = new HttpResponseStream(
                await response.Content.ReadAsStreamAsync(cancellationToken),
                response);
            return ((TResponse)(object)result, response.Headers);
        }
        if (typeof(TResponse) == typeof(FileStreamResponse))
        {
            var fileName = response.Content.Headers.ContentDisposition?.FileName ?? string.Empty;
            var result = new FileStreamResponse(new HttpResponseStream(
                await response.Content.ReadAsStreamAsync(cancellationToken),
                response), fileName);
            return ((TResponse)(object)result, response.Headers);
        }
        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        if (typeof(TResponse) == typeof(string) &&
            response.Content.Headers.ContentType?.MediaType?.StartsWith("text/", StringComparison.OrdinalIgnoreCase) == true)
        {
            return ((TResponse)(object)content, response.Headers);
        }
        if (string.IsNullOrWhiteSpace(content))
        {
            return (default, response.Headers);
        }
        var responseModel = JsonSerializer.Deserialize<TResponse>(content, SerializerOptions)!;
        return (responseModel, response.Headers);
    }

    private static JsonSerializerOptions CreateSerializerOptions()
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            TypeInfoResolver = new PrivateSetterResolver()
        };
        options.Converters.Add(new JsonStringEnumConverter());
        return options;
    }

    private static JsonSerializerOptions CreateRequestSerializerOptions()
    {
        var resolver = new DefaultJsonTypeInfoResolver();
        resolver.Modifiers.Add(typeInfo =>
        {
            if (typeInfo.Kind != JsonTypeInfoKind.Object)
            {
                return;
            }

            var keyPropertyNames = KeyRequestProperties.GetNames(typeInfo.Type);

            foreach (var property in typeInfo.Properties)
            {
                if (property.AttributeProvider is PropertyInfo propertyInfo &&
                    keyPropertyNames.Contains(propertyInfo.Name))
                {
                    property.ShouldSerialize = static (_, _) => false;
                }
            }
        });

        return new JsonSerializerOptions
        {
            TypeInfoResolver = resolver
        };
    }
}