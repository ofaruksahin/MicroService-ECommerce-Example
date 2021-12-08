using ECommerce.Web.Models.PhotoStocks;
using ECommerce.Web.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace ECommerce.Web.Services
{
    public class PhotoStockService : IPhotoStockService
    {       
        public Task<PhotoStockViewModel> UploadPhoto(IFormFile photo)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletePhoto(string photoUrl)
        {
            throw new NotImplementedException();
        }
    }
}
