using MitMediator.AutoApi.Abstractions.Attributes;

namespace MitMediator.AutoApi.Abstractions.Tests;

public class RequestInfoTests
{
    [Fact]
    public void Constructor_ShouldInitializeAllProperties()
    {
        // Arrange
        var requestType = typeof(ReachSampleRequest);

        // Act
        var info = new RequestInfo(requestType, "api");

        // Assert
        Assert.Equal("api", info.BasePath);
        Assert.Equal("sample", info.Tag);
        Assert.Equal("sample", info.PluralizedTag);
        Assert.Equal("v2", info.Version);
        Assert.Equal("Test description", info.Description);
        Assert.Equal("api/v2/samples/{key}/custom", info.Pattern);
        Assert.Equal(MethodType.Get, info.MethodType);
        Assert.Equal("application/json", info.ContentType);
        Assert.Equal(typeof(ReachSampleRequest), info.RequestType);
        Assert.False(info.IsIgnored);
        Assert.Equal(typeof(SampleResponse), info.ResponseType);
        Assert.True(info.IsKeyRequest);
        Assert.True(info.IsDisableAntiforgery);
        Assert.Equal(1, info.KeysCount);
    }
    
    [Fact]
    public void Constructor_ShouldInitializeAllProperties_WhenSuffix()
    {
        // Arrange
        var requestType = typeof(SuffixRequest);
        
        // Act
        var info = new RequestInfo(requestType, "api");

        // Act & Assert
        Assert.Equal("TestSuffix", info.Suffix);
    }

    [Fact]
    public void Constructor_ShouldThrow_WhenSuffixAndPatternConflict()
    {
        // Arrange
        var requestType = typeof(ConflictingSuffixRequest);

        // Act & Assert
        var ex = Assert.Throws<Exception>(() => new RequestInfo(requestType));
        Assert.Equal("Suffix can't be specified when a custom pattern is provided.", ex.Message);
    }

    [Theory]
    [InlineData(typeof(KeyRequest1), 1)]
    [InlineData(typeof(KeyRequest2), 2)]
    [InlineData(typeof(KeyRequest3), 3)]
    [InlineData(typeof(KeyRequest4), 4)]
    [InlineData(typeof(KeyRequest5), 5)]
    [InlineData(typeof(KeyRequest6), 6)]
    [InlineData(typeof(KeyRequest7), 7)]
    public void GetKeysCount_ShouldReturnCorrectCount(Type type, int expected)
    {
        var count = RequestInfo.GetKeysCount(type);
        Assert.Equal(expected, count);
    }

    [Fact]
    public void GetPattern_ShouldThrow_WhenKeyPlaceholderMissing()
    {
        var type = typeof(BadPatternRequest);
        var ex = Assert.Throws<Exception>(() => RequestInfo.GetPattern(type, null));
        Assert.Equal("Custom pattern must contain '{key}'.", ex.Message);
    }

    [Fact]
    public void GetHttpMethod_ShouldInferFromName()
    {
        var method = RequestInfo.GetHttpMethod(typeof(GetUserRequest));
        Assert.Equal(MethodType.Get, method);
    }

    [Fact]
    public void GetResponseType_ShouldReturnCorrectType()
    {
        var responseType = RequestInfo.GetResponseType(typeof(ReachSampleRequest));
        Assert.Equal(typeof(SampleResponse), responseType);
    }
    
    [Fact]
    public void Constructor_ShouldInitializeAllProperties_WhenRequestDontHaveAttributes()
    {
        // Arrange
        var requestType = typeof(AddBookRequest);
        
        // Act
        var info = new RequestInfo(requestType, "api");
        
        // Assert
        Assert.Equal("api", info.BasePath);
        Assert.Equal("book", info.Tag);
        Assert.Equal("books", info.PluralizedTag);
        Assert.Equal("v1", info.Version);
        Assert.Null(info.Description);
        Assert.Equal("api/v1/books", info.Pattern);
        Assert.Equal(MethodType.PostCreate, info.MethodType);
        Assert.Null(info.ContentType);
        Assert.Equal(typeof(AddBookRequest), info.RequestType);
        Assert.False(info.IsIgnored);
        Assert.Equal(typeof(Unit), info.ResponseType);
        Assert.False(info.IsKeyRequest);
        Assert.False(info.IsDisableAntiforgery);
        Assert.Null(info.KeysCount);
    }
    
    [Tag("Sample")]
    [Version("v2")]
    [Description("Test description")]
    [Pattern("api/v2/samples/{key}/custom")]
    [Method(MethodType.Get)]
    [ResponseContentType("application/json")]
    [DisableAntiforgery]
    public class ReachSampleRequest : IRequest<SampleResponse>, IKeyRequest<int>
    {
        public void SetKey(int key)
        {
            throw new NotImplementedException();
        }

        public int GetKey()
        {
            throw new NotImplementedException();
        }
    }
    
    public class AddBookRequest : IRequest;

    public class SampleResponse { }
    
    
    [Suffix("TestSuffix")]
    public class SuffixRequest : IRequest<string>
    {
    }
    
    [Pattern("api/custom")]
    [Suffix("conflict")]
    public class ConflictingSuffixRequest : IRequest<string>
    {
    }

    [Pattern("api/missing")]
    public class BadPatternRequest : IRequest<string>, IKeyRequest<int>
    {
        public void SetKey(int key)
        {
            throw new NotImplementedException();
        }

        public int GetKey()
        {
            throw new NotImplementedException();
        }
    }

    public class GetUserRequest : IRequest<string> { }

    public class KeyRequest1 : IRequest<string>, IKeyRequest<int>
    {
        public void SetKey(int key)
        {
            throw new NotImplementedException();
        }

        public int GetKey()
        {
            throw new NotImplementedException();
        }
    }
    public class KeyRequest2 : IRequest<string>, IKeyRequest<int, int>
    {
        public void SetKey1(int key)
        {
            throw new NotImplementedException();
        }

        public int GetKey1()
        {
            throw new NotImplementedException();
        }

        public void SetKey2(int key)
        {
            throw new NotImplementedException();
        }

        public int GetKey2()
        {
            throw new NotImplementedException();
        }
    }
    public class KeyRequest3 : IRequest<string>, IKeyRequest<int, int, int>
    {
        public void SetKey1(int key)
        {
            throw new NotImplementedException();
        }

        public int GetKey1()
        {
            throw new NotImplementedException();
        }

        public void SetKey2(int key)
        {
            throw new NotImplementedException();
        }

        public int GetKey2()
        {
            throw new NotImplementedException();
        }

        public void SetKey3(int key)
        {
            throw new NotImplementedException();
        }

        public int GetKey3()
        {
            throw new NotImplementedException();
        }
    }
    public class KeyRequest4 : IRequest<string>, IKeyRequest<int, int, int, int>
    {
        public void SetKey1(int key)
        {
            throw new NotImplementedException();
        }

        public int GetKey1()
        {
            throw new NotImplementedException();
        }

        public void SetKey2(int key)
        {
            throw new NotImplementedException();
        }

        public int GetKey2()
        {
            throw new NotImplementedException();
        }

        public void SetKey3(int key)
        {
            throw new NotImplementedException();
        }

        public int GetKey3()
        {
            throw new NotImplementedException();
        }

        public void SetKey4(int key)
        {
            throw new NotImplementedException();
        }

        public int GetKey4()
        {
            throw new NotImplementedException();
        }
    }
    public class KeyRequest5 : IRequest<string>, IKeyRequest<int, int, int, int, int>
    {
        public void SetKey1(int key)
        {
            throw new NotImplementedException();
        }

        public int GetKey1()
        {
            throw new NotImplementedException();
        }

        public void SetKey2(int key)
        {
            throw new NotImplementedException();
        }

        public int GetKey2()
        {
            throw new NotImplementedException();
        }

        public void SetKey3(int key)
        {
            throw new NotImplementedException();
        }

        public int GetKey3()
        {
            throw new NotImplementedException();
        }

        public void SetKey4(int key)
        {
            throw new NotImplementedException();
        }

        public int GetKey4()
        {
            throw new NotImplementedException();
        }

        public void SetKey5(int key)
        {
            throw new NotImplementedException();
        }

        public int GetKey5()
        {
            throw new NotImplementedException();
        }
    }
    public class KeyRequest6 : IRequest<string>, IKeyRequest<int, int, int, int, int, int>
    {
        public void SetKey1(int key)
        {
            throw new NotImplementedException();
        }

        public int GetKey1()
        {
            throw new NotImplementedException();
        }

        public void SetKey2(int key)
        {
            throw new NotImplementedException();
        }

        public int GetKey2()
        {
            throw new NotImplementedException();
        }

        public void SetKey3(int key)
        {
            throw new NotImplementedException();
        }

        public int GetKey3()
        {
            throw new NotImplementedException();
        }

        public void SetKey4(int key)
        {
            throw new NotImplementedException();
        }

        public int GetKey4()
        {
            throw new NotImplementedException();
        }

        public void SetKey5(int key)
        {
            throw new NotImplementedException();
        }

        public int GetKey5()
        {
            throw new NotImplementedException();
        }

        public void SetKey6(int key)
        {
            throw new NotImplementedException();
        }

        public int GetKey6()
        {
            throw new NotImplementedException();
        }
    }
    public class KeyRequest7 : IRequest<string>, IKeyRequest<int, int, int, int, int, int, int>
    {
        public void SetKey1(int key)
        {
            throw new NotImplementedException();
        }

        public int GetKey1()
        {
            throw new NotImplementedException();
        }

        public void SetKey2(int key)
        {
            throw new NotImplementedException();
        }

        public int GetKey2()
        {
            throw new NotImplementedException();
        }

        public void SetKey3(int key)
        {
            throw new NotImplementedException();
        }

        public int GetKey3()
        {
            throw new NotImplementedException();
        }

        public void SetKey4(int key)
        {
            throw new NotImplementedException();
        }

        public int GetKey4()
        {
            throw new NotImplementedException();
        }

        public void SetKey5(int key)
        {
            throw new NotImplementedException();
        }

        public int GetKey5()
        {
            throw new NotImplementedException();
        }

        public void SetKey6(int key)
        {
            throw new NotImplementedException();
        }

        public int GetKey6()
        {
            throw new NotImplementedException();
        }

        public void SetKey7(int key)
        {
            throw new NotImplementedException();
        }

        public int GetKey7()
        {
            throw new NotImplementedException();
        }
    }
}