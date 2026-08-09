using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using MitMediator.AutoApi.Abstractions;
using Moq;

namespace MitMediator.AutoApi.Tests.EndpointsMethodsTests;

public class TaskHandlerMethodsTests
{
    [Fact]
    public async Task WithGetParams_ShouldCallTaskMediator()
    {
        var mediator = new Mock<IMediator>();
        mediator
            .Setup(value => value.Send<SimpleTaskRequest, string>(It.IsAny<SimpleTaskRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync("response");

        var context = CreateContext(mediator.Object);
        var endpoint = EndpointsMethodsForTaskHandlers.WithGetParams<SimpleTaskRequest, string>(
            new RequestInfo(typeof(SimpleTaskRequest)));

        var result = await (ValueTask<IResult>)endpoint.DynamicInvoke(context, CancellationToken.None)!;

        Assert.IsType<Microsoft.AspNetCore.Http.HttpResults.Ok<string>>(result);
        mediator.Verify(value => value.Send<SimpleTaskRequest, string>(It.IsAny<SimpleTaskRequest>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task WithBodyAnd1Key_ShouldSetKeyAndCallTaskMediator()
    {
        SimpleTaskRequest? capturedRequest = null;
        var mediator = new Mock<IMediator>();
        mediator
            .Setup(value => value.Send<SimpleTaskRequest, string>(It.IsAny<SimpleTaskRequest>(), It.IsAny<CancellationToken>()))
            .Callback<SimpleTaskRequest, CancellationToken>((request, _) => capturedRequest = request)
            .ReturnsAsync("response");

        var context = CreateContext(mediator.Object);
        var endpoint = EndpointsMethodsForTaskHandlers.WithBodyAnd1Key<SimpleTaskRequest, string, int>(
            new RequestInfo(typeof(SimpleTaskRequest)));
        var request = new SimpleTaskRequest();

        var result = await (ValueTask<IResult>)endpoint.DynamicInvoke(request, 42, context, CancellationToken.None)!;

        Assert.IsType<Microsoft.AspNetCore.Http.HttpResults.Ok<string>>(result);
        Assert.Same(request, capturedRequest);
        Assert.Equal(42, capturedRequest!.Key);
    }

    private static DefaultHttpContext CreateContext(IMediator mediator)
    {
        return new DefaultHttpContext
        {
            RequestServices = new ServiceCollection()
                .AddSingleton(mediator)
                .BuildServiceProvider()
        };
    }

    private sealed class SimpleTaskRequest : IRequest<string>, IKeyRequest<int>
    {
        public int Key { get; init; }
    }
}