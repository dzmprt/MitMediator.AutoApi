using MitMediator.AutoApi.Abstractions;

namespace MitMediator.AutoApi;

internal static class Helpers
{
    internal static Type GetResponseType(Type queryType)
    {
        var requestInterface = queryType
            .GetInterfaces()
            .FirstOrDefault(i =>
                i.IsGenericType &&
                i.GetGenericTypeDefinition() == typeof(IRequest<>));

        return requestInterface!.GetGenericArguments()[0];
    }

    internal static IEnumerable<Type> GetTypesWithAttribute<TAttribute>()
        where TAttribute : Attribute
    {
        return AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type!.GetCustomAttributes(typeof(TAttribute), inherit: true).Any())!;
    }

    internal static string GetBasePattern(BaseActionAttribute arrt)
    {
        if (arrt.CustomPattern is not null)
        {
            return arrt.CustomPattern;
        }

        var tag = arrt.Tag.ToLower();
        var version = arrt.Version;
        return !string.IsNullOrWhiteSpace(arrt.Version) ? string.Concat(version, "/", tag) : tag;
    }
}