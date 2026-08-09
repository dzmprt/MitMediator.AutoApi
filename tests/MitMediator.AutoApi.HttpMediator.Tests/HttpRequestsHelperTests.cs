using MitMediator.AutoApi.Abstractions;
using MitMediator.AutoApi.HttpMediator.Extensions;

namespace MitMediator.AutoApi.HttpMediator.Tests;

public class HttpRequestsHelperTests
{
    private class SingleRequest : IRequest<int>, IKeyRequest<string>
    {
        public string Key { get; init; }
    }

    private class MultiRequest : IRequest<int>, IKeyRequest<string, string>
    {
        public string Key1 { get; init; }
        
        public string Key2 { get; init; }
    }

    private class NoKeyRequest : IRequest
    {
        public string Value { get; set; } = "value";
    }

    [Fact]
    public void GetUrl_SingleKey_ReplacesKeyPlaceholder()
    {
        var request = new SingleRequest { Key = "123" };
        
        var url = HttpRequestsHelper.GetUrl(request, "https://api.example.com");
        
        Assert.Equal("https://api.example.com/v1/single/123", url);
    }

    [Fact]
    public void GetUrl_MultipleKeys_ReplacesKeyPlaceholders()
    {
        var request = new MultiRequest { Key1 = "key1Value", Key2 = "key2Value" };
        
        var url = HttpRequestsHelper.GetUrl(request, "https://api.example.com");

        Assert.Equal("https://api.example.com/v1/multis/key1Value/key2Value", url);
    }

    [Fact]
    public void GetUrl_NoKeys_LeavesPatternUnchanged()
    {
        var request = new NoKeyRequest();
        var url = HttpRequestsHelper.GetUrl(request, "https://api.example.com");

        Assert.Equal("https://api.example.com/v1/nos/key?Value=value", url);

    }

    [Fact]
    public void ExtractKeys_OrdersCorrectly()
    {
        var keys = typeof(MultiRequest)
            .GetProperties()
            .Where(p => p.Name.StartsWith("Key"))
            .Select(p => p.Name)
            .ToArray();

        Assert.Contains("Key1", keys);
        Assert.Contains("Key2", keys);
    }
}