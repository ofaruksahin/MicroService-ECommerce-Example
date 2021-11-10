namespace ECommerce.Services.Basket.Settings
{
    public class RedisSettings : IRedisSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Password { get; set; }
    }
}
