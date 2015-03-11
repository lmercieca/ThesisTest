namespace $rootnamespace$.Aspects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class StaticMemoryCache : ICache
    {
        private class CachedObject
        {
            public string Key { get; set; }

            public object Value { get; set; }

            public DateTime CachedDate { get; set; }
        }

        private readonly TimeSpan cacheLife;
        private static readonly IList<CachedObject> Cache = new List<CachedObject>();

        public StaticMemoryCache(TimeSpan cacheLife)
        {
            this.cacheLife = cacheLife;
        }

        public object this[string key]
        {
            get
            {
                var cacheHit = Cache.FirstOrDefault(c => c.Key == key);
                if (cacheHit != null)
                {
                    if ((DateTime.Now - cacheHit.CachedDate) <= this.cacheLife)
                    {
                        return cacheHit.Value;
                    }

                    Cache.Remove(cacheHit);
                }

                return null;
            }

            set
            {
                var cacheHit = Cache.FirstOrDefault(c => c.Key == key);
                if (cacheHit != null)
                {
                    Cache.Remove(cacheHit);
                }

                Cache.Add(new CachedObject { Key = key, Value = value, CachedDate = DateTime.Now });
            }
        }
    }
}