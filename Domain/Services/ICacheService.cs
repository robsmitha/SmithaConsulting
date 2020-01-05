using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services
{
    public interface ICacheService
    {
        T GetOrSet<T>(string cacheKey, Func<T> getItemCallback) where T : class;
    }
}
