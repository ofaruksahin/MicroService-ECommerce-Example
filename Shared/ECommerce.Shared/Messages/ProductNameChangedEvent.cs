namespace ECommerce.Shared.Messages
{
    public class ProductNameChangedEvent
    {
        public string ProductId { get; set; }
        public string NewName { get; set; }
    }
}
