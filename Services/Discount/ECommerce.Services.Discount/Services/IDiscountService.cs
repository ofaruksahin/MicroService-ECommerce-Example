using ECommerce.Services.Discount.Dtos;
using ECommerce.Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Services.Discount.Services
{
    public interface IDiscountService
    {
        Task<Response<List<DiscountDto>>> GetAll();

        Task<Response<DiscountDto>> GetById(int id);

        Task<Response<NoContent>> Save(CreateDiscountDto dto);

        Task<Response<NoContent>> Update(UpdateDiscountDto dto);

        Task<Response<NoContent>> Delete(int id);

        Task<Response<DiscountDto>> GetByCodeAndUserId(string code, string userId);
    }
}
