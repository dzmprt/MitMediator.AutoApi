using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;
using MitMediator.AutoApi.Abstractions;

namespace MitMediator.AutoApi.HttpMediator;

internal class HttpRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IServiceProvider _serviceProvider;
    
    private readonly string? _baseUrl;

    private readonly string? _httpClientName;

    public HttpRequestHandler(IServiceProvider serviceProvider, string? baseUrl = null, string? httpClientName = null)
    {
        _serviceProvider = serviceProvider;
        _baseUrl = baseUrl;
        _httpClientName = httpClientName;
    }
    
    public async ValueTask<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken)
    {
        var typeRequest = typeof(TRequest);
        var pattern = RequestHelper.GetPattern(typeRequest);
        var autoApiAttribute = typeRequest.GetCustomAttribute<AutoApiAttribute>();
        var patternKeys = ExtractKeys(request);
        if (patternKeys.Any())
        {
            if (patternKeys.Length == 1)
            {
                pattern = pattern.Replace("{key}", patternKeys[0].ToString());
            }
            else
            {
                for (var i = 1; i < patternKeys.Length + 1; i++)
                {
                    pattern = pattern.Replace($"{{key{i}}}", patternKeys[0].ToString());
                }
            }
        }
        if (string.IsNullOrWhiteSpace(autoApiAttribute?.CustomPattern))
        {
            if (!string.IsNullOrWhiteSpace(_baseUrl))
            {
                pattern = string.Concat(_baseUrl, "/" ,pattern);
            }
        }
        var httpMethod = RequestHelper.GetHttpMethod(typeof(TRequest));
        var httpHeaderInjectors = _serviceProvider
            .GetServices<IHttpHeaderInjector<TRequest, TResponse>>();
        var clientFactory = _serviceProvider.GetRequiredService<IHttpClientFactory>();
        var httpClient = _httpClientName is null ? clientFactory.CreateClient() :  clientFactory.CreateClient(_httpClientName);
        foreach (var httpHeaderInjector in httpHeaderInjectors)
        {
            var header = await httpHeaderInjector.GetHeadersAsync(cancellationToken);
            if (header.HasValue)
            {
                httpClient.DefaultRequestHeaders.Add(header.Value.Item1, header.Value.Item2);   
            }
        }

        switch (httpMethod)
        {
            case HttpMethodType.PostCreate:
            case HttpMethodType.Post:
                var postResponse = await httpClient.PostAsync(pattern, new StringContent(
                    JsonSerializer.Serialize(request),
                    Encoding.UTF8,
                    "application/json"), cancellationToken);
                return (await ConvertResponseAsync<TResponse>(postResponse, cancellationToken)).Item1;
            case HttpMethodType.Put:
                var putResponse = await httpClient.PutAsync(pattern, new StringContent(
                    JsonSerializer.Serialize(request),
                    Encoding.UTF8,
                    "application/json"), cancellationToken);
                return (await ConvertResponseAsync<TResponse>(putResponse, cancellationToken)).Item1;
            case HttpMethodType.Delete:
                var deleteResponse = await httpClient.DeleteAsync(pattern + request.ToQueryString(), cancellationToken);
                return (await ConvertResponseAsync<TResponse>(deleteResponse, cancellationToken)).Item1;
            case HttpMethodType.Get:
            default:
                var getResponse = await httpClient.GetAsync(pattern + request.ToQueryString(), cancellationToken);
                return (await ConvertResponseAsync<TResponse>(getResponse, cancellationToken)).Item1;
        }
    }

    private static object[] ExtractKeys(object obj)
    {
        var type = obj.GetType();
        
        var methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public)
            .Where(m => m.Name.StartsWith("GetKey"))
            .OrderBy(m => m.Name == "GetKey" ? 0 : int.TryParse(m.Name.Substring("GetKey".Length), out var index) ? index : 1000);

        return (from method in methods where method.GetParameters().Length == 0 select method.Invoke(obj, null)).ToArray();
    }
    
    private static async ValueTask<(TResponse, HttpResponseHeaders responseHeaders)> ConvertResponseAsync<TResponse>(
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
        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        if (string.IsNullOrWhiteSpace(content))
        {
            return (default, response.Headers);
        }
        var stringEnumConverter = new JsonStringEnumConverter();
        var opts = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            TypeInfoResolver = new PrivateSetterResolver()
        };
        opts.Converters.Add(stringEnumConverter);
        var responseModel = JsonSerializer.Deserialize<TResponse>(content, opts)!;
        if (responseModel is ITotalCount)
        {
            var count = response.Headers.GetValues("X-Total-Count").FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(count))
            {
                var totalCount = (ITotalCount)responseModel;
                totalCount.SetTotalCount(int.Parse(count));
            }
        }
        return (responseModel, response.Headers);
    }
}