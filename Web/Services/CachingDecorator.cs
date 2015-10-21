using System;
using System.Globalization;
using System.Runtime.Caching;

namespace Web.Services
{
    public abstract class CachingDecorator
    {
        protected readonly System.Runtime.Caching.ObjectCache _cache;
        protected CachingDecorator(System.Runtime.Caching.ObjectCache cache)
        {
            _cache = cache;
        }
        protected string MakeKey(object itemKey)
        {
            return itemKey.GetHashCode().ToString(CultureInfo.InvariantCulture);
        }

        protected TResult Get<TResult>(string partialKey, Func<TResult> getFunc) where TResult : class
        {
            var key = string.Format("{0}_{1}", CacheKeyBase, partialKey);
            var cached = _cache.Get(key);
            if (cached != null)
            {
                
                System.Diagnostics.Debug.WriteLine("Geocoding cache hit " + key);
                return (TResult)cached;
            }

            System.Diagnostics.Debug.WriteLine("Geocoding CACHE MISS " + key);
            var result = getFunc();
            if (result != null)
            {
                _cache.Add(key, result, Policy);
            }
            return result;
        }

        public abstract string CacheKeyBase { get; }

        public virtual CacheItemPolicy Policy
        {
            get
            {
                return new CacheItemPolicy();
            }
        }
    }
}