using System;
using System.Runtime.Caching;

namespace Domain.Services
{
    public class CacheService : ICacheService
    {
        public T GetOrSet<T>(string cacheKey, Func<T> getItemCallback) where T : class
        {
            if (!(MemoryCache.Default.Get(cacheKey) is T item))
            {
                item = getItemCallback();
                MemoryCache.Default.Add(cacheKey, item, DateTime.Now.AddMinutes(10));
            }
            return item;
        }
    }
}
