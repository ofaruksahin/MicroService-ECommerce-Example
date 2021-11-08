using ECommerce.Services.Basket.Dtos;
using ECommerce.Shared.Dtos;
using System.Threading.Tasks;

namespace ECommerce.Services.Basket.Services
{
    public class BasketService : IBasketService
    {
        public Task<Response<bool>> Delete(string userId)
        {
            throw new System.NotImplementedException();
        }

        public Task<Response<BasketDto>> GetBasket(string userId)
        {
            throw new System.NotImplementedException();
        }

        public Task<Response<bool>> SaveOrUpdate(BasketDto basketDto)
        {
            throw new System.NotImplementedException();
        }
    }
}
