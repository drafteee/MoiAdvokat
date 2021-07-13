using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LawyerService.BL
{
    public interface IMemoryCacheManager
    {
        void Put(string key, object value);
        void Put(string key, object value, TimeSpan expires);

        void Remove(string key);
        void RemoveStartWith(string key);
        void Clear();

        bool TryGet<T>(string key, out T result);

        T GetOrAdd<T>(string key, T value);
        T GetOrAdd<T>(string key, T value, TimeSpan expires);
        T GetOrAdd<T>(string key, Func<T> factory);
        ValueTask<T> GetOrAddAsync<T>(string key, Func<Task<T>> factory);
        T GetOrAdd<T>(string key, Func<T> factory, TimeSpan expires);
        ValueTask<T> GetOrAddAsync<T>(string key, Func<Task<T>> factory, TimeSpan expires);

        T AddOrReplace<T>(string key, T value);
        T AddOrReplace<T>(string key, T value, TimeSpan expires);
        T AddOrReplace<T>(string key, Func<T> factory);
        ValueTask<T> AddOrReplaceAsync<T>(string key, Func<Task<T>> factory);
        T AddOrReplace<T>(string key, Func<T> factory, TimeSpan expires);
        ValueTask<T> AddOrReplaceAsync<T>(string key, Func<Task<T>> factory, TimeSpan expires);

        string GetCacheKey<T>(string key, params T[] items) where T : struct;
        string GetCacheKey(string key, params string[] items);
    }
}
