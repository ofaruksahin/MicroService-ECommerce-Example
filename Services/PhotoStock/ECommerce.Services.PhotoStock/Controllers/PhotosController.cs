using ECommerce.Services.PhotoStock.Dtos;
using ECommerce.Shared.ControllerBases;
using ECommerce.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Services.PhotoStock.Controllers
{
    public class PhotosController : CustomBaseController
    {
        [HttpPost]
        public async Task<IActionResult> PhotoSave([FromForm]IFormFile photo, CancellationToken cancellationToken)
        {
            if (photo != null && photo.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "photos", photo.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                    await photo.CopyToAsync(stream, cancellationToken);

                return CreateActionResultInstance(Response<PhotoDto>.Success(new PhotoDto() { 
                    Url = photo.FileName
                }, 200));
            }
            return CreateActionResultInstance(Response<NoContent>.Fail("Fotoğraf Yüklenemedi", 400));
        }

        [HttpDelete]
        public async Task<IActionResult> PhotoDelete(string photoUrl)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot","photos", photoUrl);
            if (!System.IO.File.Exists(path))
                return CreateActionResultInstance(Response<NoContent>.Fail("Fotoğraf bulunamadı", 404));
            System.IO.File.Delete(path);
            return CreateActionResultInstance(Response<NoContent>.Success(200));
        }
    }
}
