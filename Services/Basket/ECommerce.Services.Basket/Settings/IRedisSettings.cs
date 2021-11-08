namespace ECommerce.Services.Basket.Settings
{
    public interface IRedisSettings
    {
        string Host { get; set; }
        int Port { get; set; }
    }
}
