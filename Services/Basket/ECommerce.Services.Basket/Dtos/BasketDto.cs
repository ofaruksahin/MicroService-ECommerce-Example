using System.Collections.Generic;
using System.Linq;

namespace ECommerce.Services.Basket.Dtos
{
    public class BasketDto
    {
        public string UserId { get; set; }
        public string DiscountCode { get; set; }
        public int? DiscountRate { get; set; }
        public List<BasketItemDto> BasketItems { get; set; }

        public decimal TotalPrice
        {
            get => BasketItems.Sum(f => f.Price * f.Quantity);
        }
    }
}
