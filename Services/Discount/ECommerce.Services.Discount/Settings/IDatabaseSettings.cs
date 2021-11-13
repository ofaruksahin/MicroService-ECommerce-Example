namespace ECommerce.Services.Discount.Settings
{
    public interface IDatabaseSettings
    {
        string Host { get; set; }
        string Database { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
        string ToString();
    }
}
