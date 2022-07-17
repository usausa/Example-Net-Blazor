namespace ErrorHandleExample.Components.Authentication;

using System.Collections.Concurrent;

using Microsoft.AspNetCore.Authorization;

internal static class AttributeAuthorizeDataCache
{
    private static readonly ConcurrentDictionary<Type, IAuthorizeData[]?> Cache = new();

    public static IAuthorizeData[]? GetAuthorizeDataForType(Type type)
    {
        if (!Cache.TryGetValue(type, out var result))
        {
            result = ComputeAuthorizeDataForType(type);
            Cache[type] = result;
        }

        return result;
    }

    private static IAuthorizeData[]? ComputeAuthorizeDataForType(Type type)
    {
        var allAttributes = type.GetCustomAttributes(inherit: true);
        List<IAuthorizeData>? list = null;
        for (var i = 0; i < allAttributes.Length; i++)
        {
            if (allAttributes[i] is IAllowAnonymous)
            {
                return null;
            }

            if (allAttributes[i] is IAuthorizeData authorizeData)
            {
                list ??= new();
                list.Add(authorizeData);
            }
        }

        return list?.ToArray();
    }
}
