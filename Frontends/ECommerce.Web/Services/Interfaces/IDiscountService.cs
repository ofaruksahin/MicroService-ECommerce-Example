using ECommerce.Web.Models.Discounts;
using System.Threading.Tasks;

namespace ECommerce.Web.Services.Interfaces
{
    public interface IDiscountService
    {
        Task<DiscountViewModel> GetDiscount(string discountCode);
    }
}
