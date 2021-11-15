using ECommerce.Services.Order.Application.Dtos;
using ECommerce.Shared.Dtos;
using MediatR;
using System.Collections.Generic;

namespace ECommerce.Services.Order.Application.Queries
{
    public class GetOrdersByUserIdQuery : IRequest<Response<List<OrderDto>>>
    {
        public string UserId { get; set; }
    }
}
