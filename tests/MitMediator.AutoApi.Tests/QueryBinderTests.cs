using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using MitMediator.AutoApi.Abstractions;
using MitMediator.AutoApi.Tests.RequestsForTests.Test.Queries.GetByKey7;
using Moq;

namespace MitMediator.AutoApi.Tests;

public class QueryBinderTests
{
    [Fact]
    public void SetProperty_ShouldSetInitOnlyProperty()
    {
        var request = new RequestWithInitOnlyProperty();

        QueryBinder.SetProperty(request, nameof(RequestWithInitOnlyProperty.Key), 42);

        Assert.Equal(42, request.Key);
    }

    [Fact]
    public void SetProperty_ShouldIgnoreUnknownProperty()
    {
        var request = new RequestWithInitOnlyProperty();

        QueryBinder.SetProperty(request, "Missing", 42);

        Assert.Equal(0, request.Key);
    }

    [Fact]
    public void BindFromQuery_ShouldConvertSupportedTypesAndEmptyNullableValues()
    {
        var context = new DefaultHttpContext
        {
            Request =
            {
                Query = new QueryCollection(new Dictionary<string, StringValues>
                {
                    ["Text"] = "hello",
                    ["IntValue"] = "12",
                    ["LongValue"] = "123456789",
                    ["BoolValue"] = "true",
                    ["GuidValue"] = "4c7b31c4-5a6d-4f0c-a4c8-7c5d2d5a6e4f",
                    ["DateTimeValue"] = "2024-01-02T03:04:05",
                    ["DateTimeOffsetValue"] = "2024-01-02T03:04:05Z",
                    ["EnumValue"] = "Second",
                    ["DoubleValue"] = "12.5",
                    ["OptionalInt"] = StringValues.Empty,
                    ["Numbers"] = new StringValues(["1", "2"]),
                    ["Tags"] = new StringValues(["one", "two"])
                })
            }
        };

        var result = QueryBinder.BindFromQuery<BindingRequest>(context);

        Assert.Equal("hello", result.Text);
        Assert.Equal(12, result.IntValue);
        Assert.Equal(123456789, result.LongValue);
        Assert.True(result.BoolValue);
        Assert.Equal(Guid.Parse("4c7b31c4-5a6d-4f0c-a4c8-7c5d2d5a6e4f"), result.GuidValue);
        Assert.Equal(new DateTime(2024, 1, 2, 3, 4, 5), result.DateTimeValue);
        Assert.Equal(DateTimeOffset.Parse("2024-01-02T03:04:05Z"), result.DateTimeOffsetValue);
        Assert.Equal(TestEnum.Second, result.EnumValue);
        Assert.Equal(12.5, result.DoubleValue);
        Assert.Null(result.OptionalInt);
        Assert.Equal([1, 2], result.Numbers);
        Assert.Equal(["one", "two"], result.Tags);
    }

    [Fact]
    public async Task WithGetParamsAndSevenKeys_ShouldPassRouteKeysToMediator()
    {
        GetTestQuery? capturedRequest = null;
        var mediatorMock = new Mock<IMediator>();
        mediatorMock
            .Setup(m => m.SendAsync<GetTestQuery, string>(It.IsAny<GetTestQuery>(), It.IsAny<CancellationToken>()))
            .Callback<GetTestQuery, CancellationToken>((request, _) => capturedRequest = request)
            .ReturnsAsync(string.Empty);

        var services = new ServiceCollection()
            .AddSingleton(mediatorMock.Object)
            .BuildServiceProvider();
        var context = new DefaultHttpContext { RequestServices = services };
        var requestInfo = new RequestInfo(typeof(GetTestQuery));
        var endpoint = EndpointsMethods.WithGetParamsAnd7Keys<GetTestQuery, string, int, int, int, int, int, int, int>(requestInfo);

        var result = (ValueTask<IResult>)endpoint.DynamicInvoke(1, 2, 3, 4, 5, 6, 7, context, CancellationToken.None);

        await result;

        Assert.NotNull(capturedRequest);
        Assert.Equal(1, capturedRequest.Key1);
        Assert.Equal(2, capturedRequest.Key2);
        Assert.Equal(3, capturedRequest.Key3);
        Assert.Equal(4, capturedRequest.Key4);
        Assert.Equal(5, capturedRequest.Key5);
        Assert.Equal(6, capturedRequest.Key6);
        Assert.Equal(7, capturedRequest.Key7);
    }

    [Fact]
    public async Task WithBodyAndSevenKeys_ShouldPassRouteKeysToMediator()
    {
        GetTestQuery? capturedRequest = null;
        var mediatorMock = new Mock<IMediator>();
        mediatorMock
            .Setup(m => m.SendAsync<GetTestQuery, string>(It.IsAny<GetTestQuery>(), It.IsAny<CancellationToken>()))
            .Callback<GetTestQuery, CancellationToken>((request, _) => capturedRequest = request)
            .ReturnsAsync(string.Empty);

        var services = new ServiceCollection()
            .AddSingleton(mediatorMock.Object)
            .BuildServiceProvider();
        var context = new DefaultHttpContext { RequestServices = services };
        var request = new GetTestQuery();
        var requestInfo = new RequestInfo(typeof(GetTestQuery));
        var endpoint = EndpointsMethods.WithBodyAnd7Keys<GetTestQuery, string, int, int, int, int, int, int, int>(requestInfo);

        var result = (ValueTask<IResult>)endpoint.DynamicInvoke(request, 1, 2, 3, 4, 5, 6, 7, context, CancellationToken.None);

        await result;

        Assert.Same(request, capturedRequest);
        Assert.Equal(1, request.Key1);
        Assert.Equal(2, request.Key2);
        Assert.Equal(3, request.Key3);
        Assert.Equal(4, request.Key4);
        Assert.Equal(5, request.Key5);
        Assert.Equal(6, request.Key6);
        Assert.Equal(7, request.Key7);
    }

    private sealed class RequestWithInitOnlyProperty
    {
        public int Key { get; init; }
    }

    private sealed class BindingRequest
    {
        public string Text { get; set; } = string.Empty;
        public int IntValue { get; set; }
        public long LongValue { get; set; }
        public bool BoolValue { get; set; }
        public Guid GuidValue { get; set; }
        public DateTime DateTimeValue { get; set; }
        public DateTimeOffset DateTimeOffsetValue { get; set; }
        public TestEnum EnumValue { get; set; }
        public double DoubleValue { get; set; }
        public int? OptionalInt { get; set; }
        public int[] Numbers { get; set; } = [];
        public List<string> Tags { get; set; } = [];
    }

    private enum TestEnum
    {
        First,
        Second
    }
}