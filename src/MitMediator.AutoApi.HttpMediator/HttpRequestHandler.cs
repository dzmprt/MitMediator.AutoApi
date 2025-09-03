using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;
using MitMediator.AutoApi.Abstractions;
using MitMediator.AutoApi.HttpMediator.Extensions;

namespace MitMediator.AutoApi.HttpMediator;

internal class HttpRequestHandler<TRequest, TResponse> : IClientRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
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
        var requestInfo = new RequestInfo(typeof(TRequest));
        var httpHeaderInjectors = _serviceProvider
            .GetServices<IHttpHeaderInjector<TRequest, TResponse>>();
        foreach (var httpHeaderInjector in httpHeaderInjectors)
        {
            var header = await httpHeaderInjector.GetHeadersAsync(cancellationToken);
            if (header.HasValue)
            {
                _httpClient.DefaultRequestHeaders.Add(header.Value.Item1, header.Value.Item2);   
            }
        }

        switch (requestInfo.MethodType)
        {
            case MethodType.PostCreate:
            case MethodType.Post:
                HttpResponseMessage response;
                if (request is IFileRequest fileRequest)
                {
                    var streamContent = new StreamContent(fileRequest.File);
                    var form = new MultipartFormDataContent();
                    form.Add(streamContent, "formFile", fileRequest.FileName);

                    response = await _httpClient.PostAsync(requestUrl, form, cancellationToken);
                }
                else
                {
                    response = await _httpClient.PostAsync(requestUrl, new StringContent(
                        JsonSerializer.Serialize(request),
                        Encoding.UTF8,
                        "application/json"), cancellationToken);
                }
        
                return (await ConvertResponseAsync<TResponse>(response, cancellationToken)).Item1;
            case MethodType.Put:
                var putResponse = await _httpClient.PutAsync(requestUrl, new StringContent(
                    JsonSerializer.Serialize(request),
                    Encoding.UTF8,
                    "application/json"), cancellationToken);
                return (await ConvertResponseAsync<TResponse>(putResponse, cancellationToken)).Item1;
            case MethodType.Delete:
                var deleteResponse = await _httpClient.DeleteAsync(requestUrl, cancellationToken);
                return (await ConvertResponseAsync<TResponse>(deleteResponse, cancellationToken)).Item1;
            case MethodType.Get:
            default:
                var getResponse = await _httpClient.GetAsync(requestUrl, cancellationToken);
                return (await ConvertResponseAsync<TResponse>(getResponse, cancellationToken)).Item1;
        }
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
        if (typeof(TResponse) == typeof(Stream))
        {
            var result = await response.Content.ReadAsStreamAsync(cancellationToken);
            return ((TResponse)(object)result, response.Headers);
        }
        if (typeof(TResponse) == typeof(FileStreamResponse))
        {
            var fileName = response.Content.Headers.ContentDisposition?.FileName ?? string.Empty;
            var result = new FileStreamResponse(await response.Content.ReadAsStreamAsync(cancellationToken), fileName);
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
            if(response.Headers.TryGetValues("X-Total-Count", out var values))
            {
                var count = values.First();
                var totalCount = (ITotalCount)responseModel;
                totalCount.SetTotalCount(int.Parse(count));
            }
            else
            {
                throw new Exception("Not fount header \"X-Total-Count\", check server settings.");
            }
        }
        return (responseModel, response.Headers);
    }
}