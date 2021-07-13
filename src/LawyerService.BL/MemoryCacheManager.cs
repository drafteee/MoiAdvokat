using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LawyerService.BL
{
    public sealed class MemoryCacheManager : IMemoryCacheManager
    {
        private readonly IMemoryCache _cache = new MemoryCache(new MemoryCacheOptions());
        private CancellationTokenSource _resetCacheToken = new CancellationTokenSource();
        private static readonly object _locker = new object();
        private static SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);

        private MemoryCacheEntryOptions GetOptions(TimeSpan expires)
        {
            lock (_locker)
            {
                var options = new MemoryCacheEntryOptions();
                options.SetPriority(CacheItemPriority.Normal);
                options.AddExpirationToken(new CancellationChangeToken(_resetCacheToken.Token));
                options.SetAbsoluteExpiration(expires);
                return options;
            }
        }

        private List<string> GetKeys()
        {
            lock (_locker)
            {
                var field = typeof(MemoryCache).GetProperty("EntriesCollection", BindingFlags.NonPublic | BindingFlags.Instance);

                if (field == null)
                    throw new InvalidOperationException(nameof(GetKeys));

                var collection = (ICollection)field.GetValue(_cache);
                var items = new List<string>();

                foreach (var item in collection)
                {
                    var methodInfo = item.GetType().GetProperty("Key");
                    if (methodInfo != null)
                    {
                        var val = methodInfo.GetValue(item);
                        items.Add(val.ToString());
                    }
                }

                return items;
            }
        }

        public void Put(string key, object value)
        {
            _cache.Set(key, value);
        }

        public void Put(string key, object value, TimeSpan expires)
        {
            _cache.Set(key, value, GetOptions(expires));
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public void RemoveStartWith(string key)
        {
            foreach (var keyToRemove in GetKeys().Where(x => x.StartsWith(key)).ToArray())
            {
                Remove(keyToRemove);
            }
        }

        public void Clear()
        {
            lock (_locker)
            {
                if (_resetCacheToken != null && !_resetCacheToken.IsCancellationRequested && _resetCacheToken.Token.CanBeCanceled)
                {
                    _resetCacheToken.Cancel();
                    _resetCacheToken.Dispose();
                }

                _resetCacheToken = new CancellationTokenSource();
            }
        }

        public bool TryGet<T>(string key, out T result)
        {
            if (_cache.TryGetValue(key, out result))
            {
                return true;
            }

            result = default;
            return false;
        }

        public T GetOrAdd<T>(string key, T value)
        {
            lock (_locker)
            {
                if (!TryGet(key, out T result))
                {
                    Put(key, value);
                }

                return result;
            }
        }

        public T GetOrAdd<T>(string key, T value, TimeSpan expires)
        {
            lock (_locker)
            {
                if (!TryGet(key, out T result))
                {
                    Put(key, value, expires);
                }

                return result;
            }
        }

        public T GetOrAdd<T>(string key, Func<T> factory)
        {
            lock (_locker)
            {
                if (!TryGet(key, out T result))
                {
                    result = factory();
                    Put(key, result);
                }

                return result;
            }
        }

        public async ValueTask<T> GetOrAddAsync<T>(string key, Func<Task<T>> factory)
        {
            await _semaphoreSlim.WaitAsync();

            try
            {
                if (!TryGet(key, out T result))
                {
                    result = await factory();
                    Put(key, result);
                }

                return result;
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        public T GetOrAdd<T>(string key, Func<T> factory, TimeSpan expires)
        {
            lock (_locker)
            {
                if (!TryGet(key, out T result))
                {
                    result = factory();
                    Put(key, result, expires);
                }

                return result;
            }
        }

        public async ValueTask<T> GetOrAddAsync<T>(string key, Func<Task<T>> factory, TimeSpan expires)
        {
            await _semaphoreSlim.WaitAsync();

            try
            {
                if (!TryGet(key, out T result))
                {
                    result = await factory();
                    Put(key, result, expires);
                }

                return result;
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        public T AddOrReplace<T>(string key, T value)
        {
            lock (_locker)
            {
                if (_cache.TryGetValue(key, out _))
                {
                    Remove(key);
                }

                Put(key, value);
                return value;
            }
        }

        public T AddOrReplace<T>(string key, T value, TimeSpan expires)
        {
            lock (_locker)
            {
                if (_cache.TryGetValue(key, out _))
                {
                    Remove(key);
                }

                Put(key, value, expires);
                return value;
            }
        }

        public T AddOrReplace<T>(string key, Func<T> factory)
        {
            lock (_locker)
            {
                T result = factory();
                if (_cache.TryGetValue(key, out _))
                {
                    Remove(key);
                }

                Put(key, result);
                return result;
            }
        }

        public async ValueTask<T> AddOrReplaceAsync<T>(string key, Func<Task<T>> factory)
        {
            await _semaphoreSlim.WaitAsync();

            try
            {
                T result = await factory();
                if (_cache.TryGetValue(key, out _))
                {
                    Remove(key);
                }

                Put(key, result);
                return result;
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        public T AddOrReplace<T>(string key, Func<T> factory, TimeSpan expires)
        {
            lock (_locker)
            {
                T result = factory();
                if (_cache.TryGetValue(key, out _))
                {
                    Remove(key);
                }

                Put(key, result, expires);
                return result;
            }
        }

        public async ValueTask<T> AddOrReplaceAsync<T>(string key, Func<Task<T>> factory, TimeSpan expires)
        {
            await _semaphoreSlim.WaitAsync();

            try
            {
                T result = await factory();
                if (_cache.TryGetValue(key, out _))
                {
                    Remove(key);
                }

                Put(key, result, expires);
                return result;
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        public string GetCacheKey<T>(string key, params T[] items) where T : struct
        {
            if (items.Length == 1)
                return key + "_" + items[0];

            var builder = new StringBuilder();
            builder.Append(key + "_");
            builder.AppendJoin('_', items);

            return builder.ToString();
        }

        public string GetCacheKey(string key, params string[] items)
        {
            if (items.Length == 1)
                return key + "_" + items[0];

            var builder = new StringBuilder();
            builder.Append(key + "_");
            builder.AppendJoin('_', items);

            return builder.ToString();
        }
    }
}
