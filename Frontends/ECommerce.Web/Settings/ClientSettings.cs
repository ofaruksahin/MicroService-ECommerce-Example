namespace ECommerce.Web.Settings
{
    public class ClientSettings
    {
        public Client WebClient { get; set; }
        public Client WebClientForSecret { get; set; }
    }

    public class Client
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
