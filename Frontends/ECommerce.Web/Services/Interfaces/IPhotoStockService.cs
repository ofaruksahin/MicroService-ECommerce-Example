using ECommerce.Web.Models.PhotoStocks;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ECommerce.Web.Services.Interfaces
{
    public interface IPhotoStockService
    {
        Task<PhotoStockViewModel> UploadPhoto(IFormFile photo);
        Task<bool> DeletePhoto(string photoUrl);
    }
}
