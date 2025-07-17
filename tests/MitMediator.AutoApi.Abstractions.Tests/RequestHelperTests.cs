using System.Diagnostics.CodeAnalysis;

namespace MitMediator.AutoApi.Abstractions.Tests;

[ExcludeFromCodeCoverage]
public class GetTestQuery : IRequest<string>, IKeyRequest<int>
{
    internal int Key { get; private set; }

    public void SetKey(int key)
    {
        Key = key;
    }

    public int GetKey()
    {
        throw new NotImplementedException();
    }
}

public class RequestHelperTests
{
    [Fact]
    public void GetKeyType_ValidRequest_ReturnsGenericType()
    {
        var result = RequestHelper.GetKeyType(typeof(GetTestQuery));
        Assert.Equal(typeof(int), result);
    }

    [Fact]
    public void GetKeyType_InvalidType_Throws()
    {
        var ex = Assert.Throws<Exception>(() => RequestHelper.GetKeyType(typeof(object)));
        Assert.Contains("must implement IKeyRequest<>", ex.Message);
    }

    [Fact]
    public void IsKeyRequest_RecognizesCorrectInterface()
    {
        Assert.True(RequestHelper.IsKeyRequest(typeof(GetTestQuery)));
        Assert.False(RequestHelper.IsKeyRequest(typeof(object)));
    }

    [Fact]
    public void GetResponseType_ReturnsExpectedGenericArgument()
    {
        var result = RequestHelper.GetResponseType(typeof(GetTestQuery));
        Assert.Equal(typeof(string), result);
    }

    [AutoApi(customPattern:"customPattern")]
    class CustomPattern : IRequest
    {
        
    }
    
    [Fact]
    public void GetPattern_GetCustomPatternPatternFromAttributeCorrect()
    {
        var pattern = RequestHelper.GetPattern(typeof(CustomPattern));
        Assert.Equal("customPattern", pattern);
    }
    
    [AutoApi(customPattern:"customPattern")]
    class IncorrectCustomPatternWithKey : IRequest, IKeyRequest<int>
    {
        public int Key { get; set; }
        public void SetKey(int key)
        {
            Key = key;
        }

        public int GetKey()
        {
            return Key;
        }
    }
    
    [Fact]
    public void GetPattern__InvalidKeyPatternForOneKey_Throws()
    {
        var request = new IncorrectCustomPatternWithKey();
        var ex = Assert.Throws<Exception>(() => RequestHelper.GetPattern(request.GetType()));
        Assert.Contains("Custom pattern must contain '{key}'.", ex.Message);
        request.SetKey(1);
        Assert.Equal(1, request.GetKey());

    }
    
    [AutoApi(customPattern:"customPattern")]
    class IncorrectCustomPatternWith2Keys : IRequest, IKeyRequest<int, int>
    {
        public int Key1 { get; set; }
        
        public int Key2 { get; set; }

        
        public void SetKey1(int key)
        {
            Key1 = key;
        }

        public int GetKey1()
        {
            return Key1;
        }

        public void SetKey2(int key)
        {
            Key2 = key;
        }

        public int GetKey2()
        {
            return Key2;
        }
    }
    
    [Fact]
    public void GetPattern__InvalidKeyPatternFor2Keys_Throws()
    {
        var request = new IncorrectCustomPatternWith2Keys();
        var ex = Assert.Throws<Exception>(() => RequestHelper.GetPattern(request.GetType()));
        Assert.Contains("Custom pattern must contain '{key1}'.", ex.Message);
        request.SetKey1(1);
        request.SetKey2(2);
        Assert.Equal(1, request.GetKey1());
        Assert.Equal(2, request.GetKey2());
    }
}
