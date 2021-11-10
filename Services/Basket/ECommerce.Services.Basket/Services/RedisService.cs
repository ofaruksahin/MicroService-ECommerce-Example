using ECommerce.Services.Basket.Settings;
using StackExchange.Redis;

namespace ECommerce.Services.Basket.Services
{
    public class RedisService
    {
        private IRedisSettings _settings;

        private ConnectionMultiplexer _connectionMultiplexer;

        public RedisService(IRedisSettings settings)
        {
            _settings = settings;            
        }

        public void Connect() => _connectionMultiplexer = ConnectionMultiplexer.Connect($"{_settings.Host}:{_settings.Port},{_settings.Password}");

        public IDatabase GetDatabase(int db = 1) => _connectionMultiplexer.GetDatabase(db);
    }
}
