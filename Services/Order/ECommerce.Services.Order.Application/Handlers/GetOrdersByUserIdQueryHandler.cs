﻿using ECommerce.Services.Order.Application.Dtos;
using ECommerce.Services.Order.Application.Mapping;
using ECommerce.Services.Order.Application.Queries;
using ECommerce.Services.Order.Infrastructure;
using ECommerce.Shared.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Services.Order.Application.Handlers
{
    public class GetOrdersByUserIdQueryHandler : IRequestHandler<GetOrdersByUserIdQuery, Response<List<OrderDto>>>
    {
        private readonly OrderDbContext _dbContext;

        public GetOrdersByUserIdQueryHandler(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Response<List<OrderDto>>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
        {
            var orders = await _dbContext.Orders
                .Include(f => f.OrderItems)
                .Where(f => f.BuyerId == request.UserId)
                .ToListAsync();
            if (!orders.Any())
            {
                return Response<List<OrderDto>>.Success(new List<OrderDto>(), 200);
            }
            return Response<List<OrderDto>>.Success(ObjectMapper.Mapper.Map<List<OrderDto>>(orders), 200);
        }
    }
}
