using ECommerce.Services.Order.Application.Commands;
using ECommerce.Services.Order.Application.Dtos;
using ECommerce.Services.Order.Application.Queries;
using ECommerce.Shared.ControllerBases;
using ECommerce.Shared.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Services.Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : CustomBaseController
    {
        private readonly IMediator _mediator;
        private ISharedIdentityService _identityService;

        public OrdersController(IMediator mediator, ISharedIdentityService identityService)
        {
            _mediator = mediator;
            _identityService = identityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var response = await _mediator.Send(new GetOrdersByUserIdQuery()
            {
                UserId = _identityService.GetUserId
            });

            return CreateActionResultInstance(response);
        }

        [HttpPost]
        public async Task<IActionResult> SaveOrder(CreateOrderCommand orderCommand)
        {
            var response = await _mediator.Send(orderCommand);

            return CreateActionResultInstance(response);
        }
    }
}
