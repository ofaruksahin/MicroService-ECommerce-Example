using ECommerce.Services.Order.Infrastructure;
using ECommerce.Shared.Messages;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Services.Order.Application.Consumers
{
    public class CourseNameChangedEventConsumer : IConsumer<ProductNameChangedEvent>
    {
        private readonly OrderDbContext _orderDbContext;

        public CourseNameChangedEventConsumer(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public async Task Consume(ConsumeContext<ProductNameChangedEvent> context)
        {
            var orderItems = await _orderDbContext.OrderItems.Where(f => f.ProductId == context.Message.ProductId).ToListAsync();

            orderItems.ForEach(item =>
            {
                item.UpdateOrderItem(context.Message.NewName, item.PictureUrl, item.Price);
            });

            await _orderDbContext.SaveChangesAsync();
        }
    }
}
