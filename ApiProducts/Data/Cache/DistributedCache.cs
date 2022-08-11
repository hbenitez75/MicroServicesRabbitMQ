using System;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace ApiProducts.Data.Cache;

public static class DistributedCache
{
    private const int DefaultExpirationTimeInSeconds = 60;

    public static async Task SetRecordAsync<T>(this IDistributedCache cache, string recordId, T data,
        TimeSpan? absoluteExpireTime = null, TimeSpan? unusedExpireTime = null)
    {
        var defaultExpirationTimeInSeconds = DefaultExpirationTimeInSeconds * 2;
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromSeconds(DefaultExpirationTimeInSeconds),
            SlidingExpiration = unusedExpireTime ?? TimeSpan.FromSeconds(defaultExpirationTimeInSeconds)
        };

        var jsonData = JsonSerializer.Serialize(data);
        await cache.SetStringAsync(recordId, jsonData, options);
    }
    
    public static async Task<T> GetRecordAsync<T>(this IDistributedCache cache, string recordId)
    {
        var jsonData = await cache.GetStringAsync(recordId);

        return jsonData is null ? default(T) : JsonSerializer.Deserialize<T>(jsonData);
    }
}