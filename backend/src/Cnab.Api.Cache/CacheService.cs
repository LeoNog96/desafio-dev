using Cnab.Api.Domain.Cache;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Cnab.Api.Cache
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _cache;
        private readonly string _configuration;

        public CacheService(IDistributedCache cache, IConfiguration configuration)
        {
            _cache = cache;
            _configuration = configuration["Redis"];
        }

        public async Task<T> GetCacheAsync<T>(object referencia = null)
        {
            try
            {
                var json = await _cache.GetStringAsync(GetKey<T>(referencia));

                return json == null ? default : JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception)
            {
                return default;
            }
        }

        public async Task<IEnumerable<T>> GetEnumerableCacheAsync<T>(object referencia = null)
        {
            try
            {
                referencia = $"{referencia}_List";
                var json = await _cache.GetStringAsync(GetKey<T>(referencia));

                return json == null ? default : JsonConvert.DeserializeObject<IEnumerable<T>>(json);
            }
            catch (Exception)
            {
                return default;
            }
        }

        public async Task SetCacheAsync<T>(T entidade, TimeSpan expiraEm, object referencia = null)
        {
            try
            {
                var options = new DistributedCacheEntryOptions()
                            .SetAbsoluteExpiration(expiraEm);

                await _cache.SetStringAsync(GetKey<T>(referencia), JsonConvert.SerializeObject(entidade, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }), options);
            }
            catch (Exception) { }

        }

        public async Task SetEnumerableCacheAsync<T>(IEnumerable<T> entidade, TimeSpan expiraEm, object referencia = null)
        {
            try
            {
                var options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(expiraEm);
                referencia = $"{referencia}_List";
                await _cache.SetStringAsync(GetKey<T>(referencia), JsonConvert.SerializeObject(entidade, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }), options);
            }
            catch (Exception)
            {
            }

        }

        public async Task DeleteKeysByPrefixAsync<T>()
        {
            try
            {
                string prefixToDelete = GetKey<T>();

                ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(_configuration);
                IDatabase redisDb = redis.GetDatabase();

                var keysToDelete = redisDb.Multiplexer.GetServer(redisDb.Multiplexer.GetEndPoints()[0])
                    .Keys(pattern: prefixToDelete + "*");
                foreach (var key in keysToDelete)
                {
                    await redisDb.KeyDeleteAsync(key);
                }

                redis.Close();
            }
            catch (Exception)
            {

            }
        }

        public async Task DeleteCacheAsync<T>(object referencia = null) => await _cache.RemoveAsync(GetKey<T>(referencia));

        private static string GetKey<T>(object referencia = null) => referencia == null ? typeof(T).Name : $"{typeof(T).Name}_{referencia}";

        public async Task DeleteEnumerableCacheAsync<T>(object referencia = null) => await _cache.RemoveAsync(GetKey<T>($"{referencia}_List"));
    }
}
