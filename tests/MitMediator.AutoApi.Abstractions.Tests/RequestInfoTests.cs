using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using MitMediator.AutoApi.Abstractions.Attributes;

namespace MitMediator.AutoApi.Abstractions.Tests;

public class RequestInfoTests
{
    [Fact]
    public void KeyRequestBaseClass_DoesNotSerializeRouteKey()
    {
        var request = new UpdateRequest
        {
            Key = 42,
            Name = "Updated"
        };

        var json = JsonSerializer.Serialize(request);

        Assert.DoesNotContain("\"Key\"", json);
        Assert.Contains("\"Name\":\"Updated\"", json);
    }

    [Fact]
    public void KeyRequestBaseClasses_AllRouteKeysAreIgnored()
    {
        var keyPropertyCount = Enumerable.Range(1, 7)
            .Select(GetKeyRequestBaseType)
            .Sum(type => type.GetProperties()
                .Count(property => property.GetCustomAttributes(
                    typeof(System.Text.Json.Serialization.JsonIgnoreAttribute),
                    inherit: true).Length > 0));

        Assert.Equal(28, keyPropertyCount);
    }

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
    
    [Fact]
    public void Constructor_ShouldThrow_WhenTypeIsNotIRequest()
    {
        // Arrange
        var requestType = typeof(string);

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => new RequestInfo(requestType));
        Assert.Equal("requestType must implement IRequest<T> (Parameter 'requestType')", ex.Message);
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

    [ExcludeFromCodeCoverage]
    [Tag("Sample")]
    [Version("v2")]
    [Description("Test description")]
    [Pattern("api/v2/samples/{key}/custom")]
    [Method(MethodType.Get)]
    [ResponseContentType("application/json")]
    [DisableAntiforgery]
    public class ReachSampleRequest : IRequest<SampleResponse>, IKeyRequest<int>
    {
        public int Key { get; init; }
    }

    [ExcludeFromCodeCoverage]
    public class AddBookRequest : IRequest;

    [ExcludeFromCodeCoverage]
    public class SampleResponse { }


    [ExcludeFromCodeCoverage]
    [Suffix("TestSuffix")]
    public class SuffixRequest : IRequest<string>
    {
    }

    [ExcludeFromCodeCoverage]
    [Pattern("api/custom")]
    [Suffix("conflict")]
    public class ConflictingSuffixRequest : IRequest<string>
    {
    }

    [ExcludeFromCodeCoverage]
    [Pattern("api/missing")]
    public class BadPatternRequest : IRequest<string>, IKeyRequest<int>
    {
        public int Key { get; init; }
    }

    [ExcludeFromCodeCoverage]
    public class GetUserRequest : IRequest<string> { }


    [ExcludeFromCodeCoverage]
    public class KeyRequest1 : IRequest<string>, IKeyRequest<int>
    {
        public int Key { get; init; }
    }

    [ExcludeFromCodeCoverage]
    public class KeyRequest2 : IRequest<string>, IKeyRequest<int, int>
    {
        public int Key1 { get; init; }
        public int Key2 { get; init; }
    }

    [ExcludeFromCodeCoverage]
    public class KeyRequest3 : IRequest<string>, IKeyRequest<int, int, int>
    {
        public int Key1 { get; init; }
        public int Key2 { get; init; }
        public int Key3 { get; init; }
    }

    [ExcludeFromCodeCoverage]
    public class KeyRequest4 : IRequest<string>, IKeyRequest<int, int, int, int>
    {
        public int Key1 { get; init; }
        public int Key2 { get; init; }
        public int Key3 { get; init; }
        public int Key4 { get; init; }
    }

    [ExcludeFromCodeCoverage]
    public class KeyRequest5 : IRequest<string>, IKeyRequest<int, int, int, int, int>
    {
        public int Key1 { get; init; }
        public int Key2 { get; init; }
        public int Key3 { get; init; }
        public int Key4 { get; init; }
        public int Key5 { get; init; }
    }

    [ExcludeFromCodeCoverage]
    public class KeyRequest6 : IRequest<string>, IKeyRequest<int, int, int, int, int, int>
    {
        public int Key1 { get; init; }
        public int Key2 { get; init; }
        public int Key3 { get; init; }
        public int Key4 { get; init; }
        public int Key5 { get; init; }
        public int Key6 { get; init; }
    }

    [ExcludeFromCodeCoverage]
    public class KeyRequest7 : IRequest<string>, IKeyRequest<int, int, int, int, int, int, int>
    {
        public int Key1 { get; init; }
        public int Key2 { get; init; }
        public int Key3 { get; init; }
        public int Key4 { get; init; }
        public int Key5 { get; init; }
        public int Key6 { get; init; }
        public int Key7 { get; init; }
    }

    private sealed class UpdateRequest : KeyRequest<int>
    {
        public string Name { get; init; } = string.Empty;
    }

    private static Type GetKeyRequestBaseType(int keyCount)
    {
        return keyCount switch
        {
            1 => typeof(KeyRequest<>).MakeGenericType(typeof(int)),
            2 => typeof(KeyRequest<,>).MakeGenericType(typeof(int), typeof(int)),
            3 => typeof(KeyRequest<,,>).MakeGenericType(typeof(int), typeof(int), typeof(int)),
            4 => typeof(KeyRequest<,,,>).MakeGenericType(typeof(int), typeof(int), typeof(int), typeof(int)),
            5 => typeof(KeyRequest<,,,,>).MakeGenericType(typeof(int), typeof(int), typeof(int), typeof(int), typeof(int)),
            6 => typeof(KeyRequest<,,,,,>).MakeGenericType(typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int)),
            7 => typeof(KeyRequest<,,,,,,>).MakeGenericType(typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int)),
            _ => throw new ArgumentOutOfRangeException(nameof(keyCount))
        };
    }
}