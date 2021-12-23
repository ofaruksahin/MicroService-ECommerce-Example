using ECommerce.Shared.Dtos;
using ECommerce.Shared.Services;
using ECommerce.Web.Models.FakePayment;
using ECommerce.Web.Models.Orders;
using ECommerce.Web.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ECommerce.Web.Services
{
    public class OrderService : IOrderService
    {
        private readonly IPaymentService _paymentService;
        private readonly IBasketService _basketService;
        private readonly ISharedIdentityService _sharedIdentityService;
        private readonly HttpClient _httpClient;

        public OrderService(IPaymentService paymentService, IBasketService basketService, ISharedIdentityService sharedIdentityService, HttpClient httpClient)
        {
            _paymentService = paymentService;
            _basketService = basketService;
            _sharedIdentityService = sharedIdentityService;
            _httpClient = httpClient;
        }

        public async Task<OrderCreatedViewModel> CreateOrder(CheckoutInfoInput checkoutInfoInput)
        {
            var basket = await _basketService.Get();

            var paymentInfoInput = new PaymentInfoInput()
            {
                CardName = checkoutInfoInput.CardName,
                CardNumber = checkoutInfoInput.CardNumber,
                Expiration = checkoutInfoInput.Expiration,
                CVV = checkoutInfoInput.CVV,
                TotalPrice = basket.TotalPrice
            };

            var responsePayment = await _paymentService.ReceivePayment(paymentInfoInput);

            if (!responsePayment)
            {
                return new OrderCreatedViewModel()
                {
                    Error = "Ödeme Alınamadı",
                    IsSuccessful = false
                };
            }

            var orderCreateInput = new OrderCreateInput()
            {
                BuyerId = _sharedIdentityService.GetUserId,
                Address = new AddressCreateInput()
                {
                    District = checkoutInfoInput.District,
                    Line = checkoutInfoInput.Line,
                    Province = checkoutInfoInput.Province,
                    Street = checkoutInfoInput.Street,
                    ZipCode = checkoutInfoInput.ZipCode
                }
            };

            basket.BasketItems.ForEach(item =>
            {
                var orderItemCreateInput = new OrderItemCreateInput();
                orderItemCreateInput.ProductId = item.CourseId;
                orderItemCreateInput.Price = item.GetCurrentPrice;
                orderItemCreateInput.ProductName = item.CourseName;
                orderItemCreateInput.PictureUrl = string.Empty;
                orderCreateInput.OrderItems.Add(orderItemCreateInput);
            });

            var response = await _httpClient.PostAsJsonAsync<OrderCreateInput>("orders", orderCreateInput);

            if (!response.IsSuccessStatusCode)
            {
                return new OrderCreatedViewModel()
                {
                    Error = "Ödeme alındı fakat sipariş oluşturulamadı",
                    IsSuccessful = false
                };
            }

            var orderCreated = await response.Content.ReadFromJsonAsync<Response<OrderCreatedViewModel>>();
            orderCreated.Data.IsSuccessful = orderCreated.Data.OrderId > 0;

            await _basketService.Delete();

            return orderCreated.Data;
        }

        public async Task<List<OrderViewModel>> GetOrder()
        {
            var response = await _httpClient.GetFromJsonAsync<Response<List<OrderViewModel>>>("orders");
            return response.Data;
        }

        public async Task<OrderSuspendViewModel> SuspendOrder(CheckoutInfoInput checkoutInfoInput)
        {
            var basket = await _basketService.Get();

            var orderCreateInput = new OrderCreateInput()
            {
                BuyerId = _sharedIdentityService.GetUserId,
                Address = new AddressCreateInput()
                {
                    District = checkoutInfoInput.District,
                    Line = checkoutInfoInput.Line,
                    Province = checkoutInfoInput.Province,
                    Street = checkoutInfoInput.Street,
                    ZipCode = checkoutInfoInput.ZipCode
                }
            };

            basket.BasketItems.ForEach(item =>
            {
                var orderItemCreateInput = new OrderItemCreateInput();
                orderItemCreateInput.ProductId = item.CourseId;
                orderItemCreateInput.Price = item.GetCurrentPrice;
                orderItemCreateInput.ProductName = item.CourseName;
                orderItemCreateInput.PictureUrl = string.Empty;
                orderCreateInput.OrderItems.Add(orderItemCreateInput);
            });


            var paymentInfoInput = new PaymentInfoInput()
            {
                CardName = checkoutInfoInput.CardName,
                CardNumber = checkoutInfoInput.CardNumber,
                Expiration = checkoutInfoInput.Expiration,
                CVV = checkoutInfoInput.CVV,
                TotalPrice = basket.TotalPrice,
                Order = orderCreateInput
            };

            var responsePayment = await _paymentService.ReceivePayment(paymentInfoInput);

            if (!responsePayment)
            {
                return new OrderSuspendViewModel()
                {
                    Error = "Ödeme Alınamadı",
                    IsSuccessful = false
                };
            }

            await _basketService.Delete();

            return new OrderSuspendViewModel()
            {
                IsSuccessful = true
            };
        }
    }
}
