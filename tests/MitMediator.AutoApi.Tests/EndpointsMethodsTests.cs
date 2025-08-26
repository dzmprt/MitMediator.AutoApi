using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using MitMediator.AutoApi.Tests.RequestsForTests;
using MitMediator.AutoApi.Tests.RequestsForTests.Test.Queries.GetByKey3;
using Moq;

namespace MitMediator.AutoApi.Tests;

public class EndpointsMethodsTests
{
    [Fact]
    public async Task WithBodyAnd3Keys_ShouldCallMediatorCorrectly()
    {
        var mediatorMock = new Mock<IMediator>();
        mediatorMock.Setup(m => m.SendAsync<GetTestQuery, string>(
                It.IsAny<GetTestQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(string.Empty);

        var services = new ServiceCollection()
            .AddSingleton(mediatorMock.Object)
            .BuildServiceProvider();

        var context = new DefaultHttpContext { RequestServices = services };
        var request = new GetTestQuery();

        var del = EndpointsMethods
            .WithBodyAnd3Keys<GetTestQuery, string, int, int, int>();
        var result = (ValueTask<IResult>)del.DynamicInvoke(request, 1, 2, 3, context, CancellationToken.None);

        Assert.IsType<Microsoft.AspNetCore.Http.HttpResults.Ok<string>>(result.Result);
    }

    [Fact]
    public async Task GetMethodWithGetParams_ShouldCallMediatorCorrectly()
    {
        var request = new GetTestWithQueryParamsQuery
        {
            TestIntParam = 123,
            TestStringParam = "TestStringParam",
            TestNullableIntParam = null,
            TestNullableIntParam2 = 1234,
            TestNullableStringParam = null,
            TestNullableStringParam2 = "TestNullableStringParam2",
            DateTimeLocalParam = new DateTime(2024, 12,29, 13, 59,22,123, DateTimeKind.Local),
            UtcDateTimeParam = new DateTime(2024, 12, 29, 20, 22, 0, DateTimeKind.Utc),
            DateTimeOffsetParam =new DateTimeOffset(2024, 12, 29, 20, 22, 2, 123, TimeSpan.Zero),
            ArrayParam = ["1", "2"],
            ListParam = ["3", "4"],
            InnerObject = new GetTestWithQueryInnerObject
            {
                Name = "Inner object name"
            },
            TestEnumParam = GetTestWithQueryParamsEnum.TestEnum2
        };

        var expectedResult = request;
        
        var queryParams = new Dictionary<string, StringValues>
        {
            ["TestIntParam"] = request.TestIntParam.ToString(),
            ["TestStringParam"] = request.TestStringParam,
            ["TestNullableIntParam2"] = request.TestNullableIntParam2.ToString(),
            ["TestNullableStringParam2"] = request.TestNullableStringParam2,
            ["DateTimeLocalParam"] = request.DateTimeLocalParam.ToString("yyyy-MM-ddTHH:mm:ss.fff"),
            ["UtcDateTimeParam"] = request.UtcDateTimeParam.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
            ["DateTimeOffsetParam"] = request.DateTimeOffsetParam.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
            ["ArrayParam"] = new(request.ArrayParam),
            ["ListParam"] = new(request.ListParam.ToArray()),
            ["InnerObject.Name"] = request.InnerObject.Name,
            ["TestEnumParam"] = request.TestEnumParam.ToString()
        };
        
        var mediatorMock = new Mock<IMediator>();
        mediatorMock.Setup(m => m.SendAsync<GetTestWithQueryParamsQuery, GetTestWithQueryParamsQuery>(
                It.IsAny<GetTestWithQueryParamsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Func<GetTestWithQueryParamsQuery, CancellationToken, GetTestWithQueryParamsQuery>((queryRequest, ct) => queryRequest));
        
        var services = new ServiceCollection()
            .AddSingleton(mediatorMock.Object)
            .AddLogging() 
            .BuildServiceProvider();
        
        var context = new DefaultHttpContext
        {
            RequestServices = services,
            Request =
            {
                Query = new QueryCollection(queryParams)
            }
        };

        var stream = new MemoryStream();
        context.Response.Body = stream;
        var del = EndpointsMethods
            .WithGetParams<GetTestWithQueryParamsQuery, GetTestWithQueryParamsQuery>();
        var result = await (ValueTask<IResult>)del.DynamicInvoke(context, CancellationToken.None);
        await result.ExecuteAsync(context);
        
        stream.Seek(0, SeekOrigin.Begin);
        using var reader = new StreamReader(stream);
        var json = await reader.ReadToEndAsync();
        var responseObject = JsonSerializer.Deserialize<GetTestWithQueryParamsQuery>(json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        
        Assert.Equal(expectedResult.TestIntParam, responseObject.TestIntParam);
        Assert.Equal(expectedResult.TestStringParam, responseObject.TestStringParam);
        Assert.Equal(expectedResult.TestNullableIntParam, responseObject.TestNullableIntParam);
        Assert.Equal(expectedResult.TestNullableIntParam2, responseObject.TestNullableIntParam2);
        Assert.Equal(expectedResult.TestNullableStringParam, responseObject.TestNullableStringParam);
        Assert.Equal(expectedResult.TestNullableStringParam2, responseObject.TestNullableStringParam2);
        Assert.Equal(expectedResult.DateTimeLocalParam, responseObject.DateTimeLocalParam);
        Assert.Equal(expectedResult.UtcDateTimeParam, responseObject.UtcDateTimeParam.ToUniversalTime());
        Assert.Equal(expectedResult.DateTimeOffsetParam, responseObject.DateTimeOffsetParam);
        Assert.Equal(expectedResult.ArrayParam, responseObject.ArrayParam);
        Assert.Equal(expectedResult.ListParam, responseObject.ListParam);
        Assert.Equal(expectedResult.InnerObject.Name, responseObject.InnerObject.Name);
    }
    
    [Fact]
    public async Task GetMethodWithGetParams_AllNullableParamsIsNull_ShouldCallMediatorCorrectly()
    {
        var request = new GetWithQueryAllNullableParamsQuery
        {
            TestIntParam = null,
            TestStringParam = null,
            TestNullableIntParam = null,
            TestNullableIntParam2 = null,
            TestNullableStringParam = null,
            TestNullableStringParam2 = null,
            DateTimeLocalParam = null,
            UtcDateTimeParam = null,
            DateTimeOffsetParam = null,
            ArrayParam = null,
            ListParam = null,
            InnerObject = null,
            TestEnumParam = null
        };

        var expectedResult = request;
        
        var queryParams = new Dictionary<string, StringValues>();
        
        var mediatorMock = new Mock<IMediator>();
        mediatorMock.Setup(m => m.SendAsync<GetWithQueryAllNullableParamsQuery, GetWithQueryAllNullableParamsQuery>(
                It.IsAny<GetWithQueryAllNullableParamsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Func<GetWithQueryAllNullableParamsQuery, CancellationToken, GetWithQueryAllNullableParamsQuery>((queryRequest, ct) => queryRequest));
        
        var services = new ServiceCollection()
            .AddSingleton(mediatorMock.Object)
            .AddLogging() 
            .BuildServiceProvider();
        
        var context = new DefaultHttpContext
        {
            RequestServices = services,
            Request =
            {
                Query = new QueryCollection(queryParams)
            }
        };

        var stream = new MemoryStream();
        context.Response.Body = stream;
        var del = EndpointsMethods
            .WithGetParams<GetWithQueryAllNullableParamsQuery, GetWithQueryAllNullableParamsQuery>();
        var result = await (ValueTask<IResult>)del.DynamicInvoke(context, CancellationToken.None);
        await result.ExecuteAsync(context);
        
        stream.Seek(0, SeekOrigin.Begin);
        using var reader = new StreamReader(stream);
        var json = await reader.ReadToEndAsync();
        var responseObject = JsonSerializer.Deserialize<GetWithQueryAllNullableParamsQuery>(json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        
        Assert.Equal(expectedResult.TestIntParam, responseObject.TestIntParam);
        Assert.Equal(expectedResult.TestStringParam, responseObject.TestStringParam);
        Assert.Equal(expectedResult.TestNullableIntParam, responseObject.TestNullableIntParam);
        Assert.Equal(expectedResult.TestNullableIntParam2, responseObject.TestNullableIntParam2);
        Assert.Equal(expectedResult.TestNullableStringParam, responseObject.TestNullableStringParam);
        Assert.Equal(expectedResult.TestNullableStringParam2, responseObject.TestNullableStringParam2);
        Assert.Equal(expectedResult.DateTimeLocalParam, responseObject.DateTimeLocalParam);
        Assert.Equal(expectedResult.UtcDateTimeParam, responseObject.UtcDateTimeParam?.ToUniversalTime());
        Assert.Equal(expectedResult.DateTimeOffsetParam, responseObject.DateTimeOffsetParam);
        Assert.Equal(expectedResult.ArrayParam, responseObject.ArrayParam);
        Assert.Equal(expectedResult.ListParam, responseObject.ListParam);
        Assert.Equal(expectedResult.InnerObject?.Name, responseObject.InnerObject?.Name);
    }
    
    
    [Fact]
    public async Task GetMethodWithGetParams_PartOfNullableParamsIsNull_ShouldCallMediatorCorrectly()
    {
        var request = new GetWithQueryAllNullableParamsQuery
        {
            TestIntParam = 123,
            TestStringParam = "TestStringParam",
            TestNullableIntParam = null,
            TestNullableIntParam2 = null,
            TestNullableStringParam = null,
            TestNullableStringParam2 = null,
            DateTimeLocalParam = null,
            UtcDateTimeParam = null,
            DateTimeOffsetParam = null,
            ArrayParam = null,
            ListParam = ["3", "4"],
            InnerObject = null,
            TestEnumParam = null
        };

        var expectedResult = request;
        
        var queryParams = new Dictionary<string, StringValues>
        {
            ["TestIntParam"] = request.TestIntParam.ToString(),
            ["TestStringParam"] = request.TestStringParam,
            ["ListParam"] = new(request.ListParam.ToArray())
        };
        
        var mediatorMock = new Mock<IMediator>();
        mediatorMock.Setup(m => m.SendAsync<GetWithQueryAllNullableParamsQuery, GetWithQueryAllNullableParamsQuery>(
                It.IsAny<GetWithQueryAllNullableParamsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Func<GetWithQueryAllNullableParamsQuery, CancellationToken, GetWithQueryAllNullableParamsQuery>((queryRequest, ct) => queryRequest));
        
        var services = new ServiceCollection()
            .AddSingleton(mediatorMock.Object)
            .AddLogging() 
            .BuildServiceProvider();
        
        var context = new DefaultHttpContext
        {
            RequestServices = services,
            Request =
            {
                Query = new QueryCollection(queryParams)
            }
        };

        var stream = new MemoryStream();
        context.Response.Body = stream;
        var del = EndpointsMethods
            .WithGetParams<GetWithQueryAllNullableParamsQuery, GetWithQueryAllNullableParamsQuery>();
        var result = await (ValueTask<IResult>)del.DynamicInvoke(context, CancellationToken.None);
        await result.ExecuteAsync(context);
        
        stream.Seek(0, SeekOrigin.Begin);
        using var reader = new StreamReader(stream);
        var json = await reader.ReadToEndAsync();
        var responseObject = JsonSerializer.Deserialize<GetWithQueryAllNullableParamsQuery>(json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        
        Assert.Equal(expectedResult.TestIntParam, responseObject.TestIntParam);
        Assert.Equal(expectedResult.TestStringParam, responseObject.TestStringParam);
        Assert.Equal(expectedResult.TestNullableIntParam, responseObject.TestNullableIntParam);
        Assert.Equal(expectedResult.TestNullableIntParam2, responseObject.TestNullableIntParam2);
        Assert.Equal(expectedResult.TestNullableStringParam, responseObject.TestNullableStringParam);
        Assert.Equal(expectedResult.TestNullableStringParam2, responseObject.TestNullableStringParam2);
        Assert.Equal(expectedResult.DateTimeLocalParam, responseObject.DateTimeLocalParam);
        Assert.Equal(expectedResult.UtcDateTimeParam, responseObject.UtcDateTimeParam?.ToUniversalTime());
        Assert.Equal(expectedResult.DateTimeOffsetParam, responseObject.DateTimeOffsetParam);
        Assert.Equal(expectedResult.ArrayParam, responseObject.ArrayParam);
        Assert.Equal(expectedResult.ListParam, responseObject.ListParam);
        Assert.Equal(expectedResult.InnerObject?.Name, responseObject.InnerObject?.Name);
    }
}
