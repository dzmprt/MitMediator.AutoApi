namespace MitMediator.AutoApi;

internal static class ServiceProviderExtensions
{
    public static bool IsRequestHandlerTaskValueRegistered(this IServiceProvider provider, Type requestType)
    {
        var responseType = requestType.GetInterfaces()
            .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequest<>))
            ?.GetGenericArguments()[0];

        if (responseType == null)
            return false;

        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(requestType, responseType);

        using var scope = provider.CreateScope();
        var service = scope.ServiceProvider.GetService(handlerType);
        return service != null;
    }
    
    public static bool IsRequestHandlerTaskRegistered(this IServiceProvider provider, Type requestType)
    {
        var responseType = requestType.GetInterfaces()
            .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequest<>))
            ?.GetGenericArguments()[0];

        if (responseType == null)
            return false;

        var handlerType = typeof(MitMediator.Tasks.IRequestHandler<,>).MakeGenericType(requestType, responseType);

        using var scope = provider.CreateScope();
        var service = scope.ServiceProvider.GetService(handlerType);
        return service != null;
    }
}