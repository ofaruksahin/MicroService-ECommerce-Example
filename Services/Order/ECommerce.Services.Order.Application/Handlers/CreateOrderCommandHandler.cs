using ECommerce.Services.Order.Application.Commands;
using ECommerce.Services.Order.Application.Dtos;
using ECommerce.Services.Order.Domain.OrderAggregate;
using ECommerce.Services.Order.Infrastructure;
using ECommerce.Shared.Dtos;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Services.Order.Application.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Response<CreatedOrderDto>>
    {
        private readonly OrderDbContext _dbContext;

        public CreateOrderCommandHandler(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Response<CreatedOrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var newAddress = new Address(
                request.Address.Province,
                request.Address.District,
                request.Address.Street,
                request.Address.ZipCode,
                request.Address.Line
                );

            var newOrder = new Domain.OrderAggregate.Order(request.BuyerId, newAddress);
            request.OrderItems.ForEach(item =>
            {
                newOrder.AddOrderItem(item.ProductId, item.ProductName, item.PictureUrl, item.Price);
            });

            _dbContext.Orders.Add(newOrder);

            var result = await _dbContext.SaveChangesAsync();

            return result > 0 ? Response<CreatedOrderDto>.Success(new () { OrderId = newOrder.Id }, 200) : Response<CreatedOrderDto>.Fail("Order not created", 400);
        }
    }
}
