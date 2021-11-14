using AutoMapper;
using ECommerce.Services.Order.Application.Dtos;
using ECommerce.Services.Order.Domain.OrderAggregate;

namespace ECommerce.Services.Order.Application.Mapping
{
    public class CustomMapping : Profile
    {
        public CustomMapping()
        {
            CreateMap<Domain.OrderAggregate.Order, OrderDto>().ReverseMap();
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}
