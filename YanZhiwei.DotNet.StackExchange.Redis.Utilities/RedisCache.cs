using StackExchange.Redis;
using YanZhiwei.DotNet.Newtonsoft.Json.Utilities;

namespace YanZhiwei.DotNet.StackExchange.Redis.Utilities
{
    public class RedisCache
    {
        private readonly IDatabase _db;
        private readonly ConnectionMultiplexer _connectionMultiplexer;

        public RedisCache()
        {
            ConfigurationOptions options = new ConfigurationOptions()
            {
                Ssl = true,
                AllowAdmin = true
            };

            //options.EndPoints.Add("127.0.0.1",6379);
            _connectionMultiplexer = ConnectionMultiplexer.Connect("127.0.0.1,6379");
            _db = _connectionMultiplexer.GetDatabase(0);
        }

        public T Get<T>(string key) where T : class
        {
            RedisValue value = _db.StringGet(key);
            if (!value.HasValue)
            {
                return default(T);
            }

            return JsonHelper.Deserialize<T>(value);
        }

        public bool Set<T>(string key, T value) where T : class
        {
            if (string.IsNullOrEmpty(key))
            {
                return false;
            }
            RedisValue result = JsonHelper.Serialize(value);

            return _db.StringSet(key, result);
        }

        public bool Replace<T>(string key, T value) where T : class
        {
            return Set(key, value);
        }

        //public Dictionary<string, T> GetAll<T>(IEnumerable<string> keys)
        //{
        //    _db.StringGetRange();

        //}
    }
}