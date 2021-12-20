using ECommerce.Web.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Web.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderCreatedViewModel> CreateOrder(CheckoutInfoInput checkoutInfoInput);
        Task SuspendOrder(CheckoutInfoInput checkoutInfoInput);
        Task<List<OrderViewModel>> GetOrder();
    }
}
