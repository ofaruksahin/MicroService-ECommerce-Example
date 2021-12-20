using System;
using System.Collections.Generic;

namespace ECommerce.Web.Models.Orders
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public DateTime CreatedTime { get; set; }
        //public AddressDto Address { get; set; }
        public string BuyerId { get; set; }
        public List<OrderItemViewModel> OrderItems { get; set; }

        public OrderViewModel()
        {
            OrderItems = new List<OrderItemViewModel>();
        }
    }
}
