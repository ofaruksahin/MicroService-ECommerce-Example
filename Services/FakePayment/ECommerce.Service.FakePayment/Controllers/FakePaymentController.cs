using ECommerce.Service.FakePayment.Models;
using ECommerce.Shared.ControllerBases;
using ECommerce.Shared.Dtos;
using ECommerce.Shared.Messages;
using ECommerce.Shared.Services;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Service.FakePayment.Controllers
{
    public class FakePaymentController : CustomBaseController
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public FakePaymentController(ISendEndpointProvider sendEndpointProvider)
        {
            _sendEndpointProvider = sendEndpointProvider;
        }

        [HttpPost]
        public async Task<IActionResult> ReceivePayments(PaymentDto dto)
        {
            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:create-order-service"));

            var createOrderMessageCommand = new CreateOrderMessageCommand
            {
                BuyerId = dto.Order.BuyerId,
                Province = dto.Order.Address.Province,
                District = dto.Order.Address.District,
                Street = dto.Order.Address.Street,
                Line = dto.Order.Address.Line,
                ZipCode = dto.Order.Address.ZipCode,
                OrderItems = dto.Order.OrderItems.Select(f => new OrderItem
                {
                    PictureUrl = f.PictureUrl,
                    Price = f.Price,
                    ProductId = f.ProductId,
                    ProductName = f.ProductName
                }).ToList()
            };

            await sendEndpoint.Send<CreateOrderMessageCommand>(createOrderMessageCommand);

            return CreateActionResultInstance<NoContent>(Shared.Dtos.Response<NoContent>.Success(200));
        }
    }
}
