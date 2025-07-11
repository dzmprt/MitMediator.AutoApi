using System.Net;
using MitMediator;
using MitMediator.AutoApi;
using MitMediator.AutoApi.HttpMediator;
using Moq;
using Moq.Protected;

public class HttpMediatorTests
{
    public class SampleRequest : IRequest<string> { }
    
    public class SampleVoidRequest : IRequest{ }
    
    [Fact]
    public async Task SendAsync_ShouldInvokePipelineAndReturnResponse()
    {
        var serviceProviderMock = new Mock<IServiceProvider>();
        var expectedResponse = "test-response";
        var cancellationToken = CancellationToken.None;
        
        var httpClientFactoryMock = new Mock<IHttpClientFactory>();

        var httpMessageHandlerMock = new Mock<HttpMessageHandler>();
        httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("\"test-response\"")
            });

        var httpClient = new HttpClient(httpMessageHandlerMock.Object);

        httpClientFactoryMock
            .Setup(f => f.CreateClient(It.IsAny<string>()))
            .Returns(httpClient);

        serviceProviderMock
            .Setup(sp => sp.GetService(typeof(IHttpClientFactory)))
            .Returns(httpClientFactoryMock.Object);

        var request = new SampleRequest();
        var behaviorMock = new Mock<IPipelineBehavior<SampleRequest, string>>();
        
        serviceProviderMock
            .Setup(sp => sp.GetService(typeof(IEnumerable<IPipelineBehavior<SampleRequest, string>>)))
            .Returns(new[] { behaviorMock.Object });
        
        var headerInjectorMock = new Mock<IHttpHeaderInjector<SampleRequest, string>>();

        serviceProviderMock
            .Setup(sp => sp.GetService(typeof(IEnumerable<IHttpHeaderInjector<SampleRequest, string>>)))
            .Returns(new[] { headerInjectorMock.Object });
        
        behaviorMock
            .Setup(b => b.HandleAsync(request, It.IsAny<IRequestHandlerNext<SampleRequest, string>>(), It.IsAny<CancellationToken>()))
            .Returns(async (SampleRequest _, IRequestHandlerNext<SampleRequest, string> next, CancellationToken _) => await next.InvokeAsync(request, cancellationToken));

        var mediator = new HttpMediator(serviceProviderMock.Object, "https://localhost", "default");

        // Act
        var result = await mediator.SendAsync<SampleRequest, string>(request, cancellationToken);

        // Assert
        Assert.Equal(expectedResponse, result);
        behaviorMock.Verify(b => b.HandleAsync(It.IsAny<SampleRequest>(), It.IsAny<IRequestHandlerNext<SampleRequest, string>>(), cancellationToken), Times.Once);
    }
    
    [Fact]
    public async Task SendAsync_ShouldInvokePipelineNoResponse()
    {
        var serviceProviderMock = new Mock<IServiceProvider>();
        var expectedResponse = "test-response";
        var cancellationToken = CancellationToken.None;
        
        var httpClientFactoryMock = new Mock<IHttpClientFactory>();

        var httpMessageHandlerMock = new Mock<HttpMessageHandler>();
        httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
            });

        var httpClient = new HttpClient(httpMessageHandlerMock.Object);

        httpClientFactoryMock
            .Setup(f => f.CreateClient(It.IsAny<string>()))
            .Returns(httpClient);

        serviceProviderMock
            .Setup(sp => sp.GetService(typeof(IHttpClientFactory)))
            .Returns(httpClientFactoryMock.Object);

        var request = new SampleVoidRequest();
        var behaviorMock = new Mock<IPipelineBehavior<SampleVoidRequest, Unit>>();
        
        serviceProviderMock
            .Setup(sp => sp.GetService(typeof(IEnumerable<IPipelineBehavior<SampleVoidRequest, Unit>>)))
            .Returns(new[] { behaviorMock.Object });
        
        var headerInjectorMock = new Mock<IHttpHeaderInjector<SampleVoidRequest, Unit>>();

        serviceProviderMock
            .Setup(sp => sp.GetService(typeof(IEnumerable<IHttpHeaderInjector<SampleVoidRequest, Unit>>)))
            .Returns(new[] { headerInjectorMock.Object });
        
        behaviorMock
            .Setup(b => b.HandleAsync(request, It.IsAny<IRequestHandlerNext<SampleVoidRequest, Unit>>(), It.IsAny<CancellationToken>()))
            .Returns(async (SampleVoidRequest _, IRequestHandlerNext<SampleVoidRequest, Unit> next, CancellationToken _) => await next.InvokeAsync(request, cancellationToken));

        var mediator = new HttpMediator(serviceProviderMock.Object, "https://localhost", "default");

        // Act
        var result = await mediator.SendAsync(request, cancellationToken);

        // Assert
        Assert.Equal(Unit.Value, result);
        behaviorMock.Verify(b => b.HandleAsync(It.IsAny<SampleVoidRequest>(), It.IsAny<IRequestHandlerNext<SampleVoidRequest, Unit>>(), cancellationToken), Times.Once);
    }
}