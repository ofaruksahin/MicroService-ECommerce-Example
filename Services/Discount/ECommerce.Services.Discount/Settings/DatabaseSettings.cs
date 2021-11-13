namespace ECommerce.Services.Discount.Settings
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string Host { get; set; }
        public string Database { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public override string ToString()
        {
            return $"Server={Host};Database={Database};Uid={UserName};Pwd={Password};";
        }
    }
}
