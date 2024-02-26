using Newtonsoft.Json;
using StackExchange.Redis;

namespace BLL.Services
{
    public class CacheService
    {
        private readonly ConnectionMultiplexer redisConnection;
        private readonly IDatabase database;

        public CacheService(string connectionString)
        {
            redisConnection = ConnectionMultiplexer.Connect(connectionString);
            database = redisConnection.GetDatabase();
        }

        public T Get<T>(string key)
        {
            var value = database.StringGet(key);
            if (value.HasValue)
            {
                return JsonConvert.DeserializeObject<T>(value);
            }

            return default(T);
        }

        public void Set<T>(string key, T value, TimeSpan? expiry = null)
        {
            database.StringSet(key, JsonConvert.SerializeObject(value), expiry);
        }
    }
}
