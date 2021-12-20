using ECommerce.Web.Models.Orders;
using ECommerce.Web.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Web.Services
{
    public class OrderService : IOrderService
    {
        public Task<OrderCreatedViewModel> CreateOrder(CheckoutInfoInput checkoutInfoInput)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<OrderViewModel>> GetOrder()
        {
            throw new System.NotImplementedException();
        }

        public Task SuspendOrder(CheckoutInfoInput checkoutInfoInput)
        {
            throw new System.NotImplementedException();
        }
    }
}
