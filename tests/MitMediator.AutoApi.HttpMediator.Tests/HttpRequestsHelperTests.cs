using MitMediator.AutoApi.Abstractions;
using MitMediator.AutoApi.HttpMediator.Extensions;

namespace MitMediator.AutoApi.HttpMediator.Tests;

public class HttpRequestsHelperTests
{
    private class SingleRequest : IRequest<int>, IKeyRequest<string>
    {
        private string Key { get; set; }
        
        public void SetKey(string key)
        {
            Key = key;
        }

        public string GetKey() => Key;
    }

    private class MultiRequest : IRequest<int>, IKeyRequest<string, string>
    {
        private string Key1 { get; set; }
        
        private string Key2 { get; set; }
        
        public void SetKey1(string key)
        {
            Key1 = key;
        }

        public string GetKey1() => Key1;

        public void SetKey2(string key)
        {
            Key2 = key;
        }

        public string GetKey2() => Key2;
    }

    private class NoKeyRequest : IRequest
    {
        public string Value { get; set; } = "value";
    }

    [Fact]
    public void GetUrl_SingleKey_ReplacesKeyPlaceholder()
    {
        var request = new SingleRequest();
        request.SetKey("123");
        
        var url = HttpRequestsHelper.GetUrl(request, "https://api.example.com");
        
        Assert.Equal("https://api.example.com/v1/single/123", url);
    }

    [Fact]
    public void GetUrl_MultipleKeys_ReplacesKeyPlaceholders()
    {
        var request = new MultiRequest();
        request.SetKey1("key1Value");
        request.SetKey2("key2Value");
        
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
            .GetMethods()
            .Where(m => m.Name.StartsWith("GetKey"))
            .Select(m => m.Name)
            .ToArray();

        Assert.Contains("GetKey1", keys);
        Assert.Contains("GetKey2", keys);
    }
}