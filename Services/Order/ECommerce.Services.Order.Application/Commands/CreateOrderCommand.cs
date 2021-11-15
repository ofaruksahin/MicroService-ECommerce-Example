using ECommerce.Services.Order.Application.Dtos;
using ECommerce.Shared.Dtos;
using MediatR;
using System.Collections.Generic;

namespace ECommerce.Services.Order.Application.Commands
{
    public class CreateOrderCommand :  IRequest<Response<CreatedOrderDto>>
    {
        public string BuyerId { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
        public AddressDto Address { get; set; }
    }
}
