using System.Globalization;

namespace MitMediator.AutoApi;

internal static class RouteValueEncoder
{
    public static string EncodeRequestKey<TRequest>(
        TRequest request,
        string propertyName)
    {
        var property = typeof(TRequest).GetProperty(propertyName)
            ?? throw new InvalidOperationException(
                $"Request type '{typeof(TRequest).FullName}' does not contain key property '{propertyName}'.");

        return Encode(property.GetValue(request));
    }

    public static string Encode(object? value)
    {
        if (value is null)
        {
            return "null";
        }

        var routeValue = value switch
        {
            int i => i.ToString(CultureInfo.InvariantCulture),
            long l => l.ToString(CultureInfo.InvariantCulture),
            float f => f.ToString(CultureInfo.InvariantCulture),
            double d => d.ToString(CultureInfo.InvariantCulture),
            decimal m => m.ToString(CultureInfo.InvariantCulture),
            short s => s.ToString(CultureInfo.InvariantCulture),
            byte b => b.ToString(CultureInfo.InvariantCulture),
            DateTime dt => dt.ToString("O", CultureInfo.InvariantCulture),
            DateTimeOffset dto => dto.ToString("O", CultureInfo.InvariantCulture),
            _ => value.ToString() ?? "null"
        };

        return Uri.EscapeDataString(routeValue);
    }
}
