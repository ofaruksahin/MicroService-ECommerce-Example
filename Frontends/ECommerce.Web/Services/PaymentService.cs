using ECommerce.Web.Models.FakePayment;
using ECommerce.Web.Services.Interfaces;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ECommerce.Web.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly HttpClient _httpClient;

        public PaymentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> ReceivePayment(PaymentInfoInput paymentInfoInput)
        {
            var response = await _httpClient.PostAsJsonAsync<PaymentInfoInput>("fakepayment",paymentInfoInput);
            return response.IsSuccessStatusCode;
        }
    }
}
