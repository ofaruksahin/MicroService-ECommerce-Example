using ECommerce.Shared.Dtos;
using ECommerce.Web.Models.Discounts;
using ECommerce.Web.Services.Interfaces;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ECommerce.Web.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly HttpClient _httpClient;

        public DiscountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<DiscountViewModel> GetDiscount(string discountCode)
        {
            //[controller]/[action] /{ code}
            var response = await _httpClient.GetAsync($"discounts/GetByCode/{discountCode}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return (await response.Content.ReadFromJsonAsync<Response<DiscountViewModel>>()).Data;
        }
    }
}
