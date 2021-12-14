using ECommerce.Shared.Dtos;
using ECommerce.Web.Helpers;
using ECommerce.Web.Models.Catalogs;
using ECommerce.Web.Services.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ECommerce.Web.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _httpClient;
        private readonly IPhotoStockService _photoStockService;
        private readonly PhotoHelper _photoHelper;

        public CatalogService(
            HttpClient httpClient,
            IPhotoStockService photoStockService,
            PhotoHelper photoHelper)
        {
            _httpClient = httpClient;
            _photoStockService = photoStockService;
            _photoHelper = photoHelper;
        }

        public async Task<bool> CreateCourseAsync(CourseCreateInput courseCreateInput)
        {
            var resultPhotoService = await _photoStockService.UploadPhoto(courseCreateInput.PhotoFormFile);
            if (resultPhotoService != null)
            {
                courseCreateInput.Picture = resultPhotoService.Url;
            }
            var response = await _httpClient.PostAsJsonAsync("courses", courseCreateInput);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCourseAsync(string courseId)
        {
            var response = await _httpClient.DeleteAsync($"courses/{courseId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<CategoryViewModel>> GetAllCategoriesAsync()
        {
            var response = await _httpClient.GetAsync("category");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return (await response.Content.ReadFromJsonAsync<Response<List<CategoryViewModel>>>()).Data;
        }

        public async Task<List<CourseViewModel>> GetAllCourseAsync()
        {
            var response = await _httpClient.GetAsync("courses");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var courses = (await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>()).Data;

            courses.ForEach(item => item.StockPictureUrl = _photoHelper.GetPhotoStockUrl(item.Picture));

            return courses;
        }

        public async Task<List<CourseViewModel>> GetAllCourseByUserIdAsync(string userId)
        {
            var response = await _httpClient.GetAsync($"courses/user/{userId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var courses = (await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>()).Data;

            courses.ForEach(item =>
            {
                item.StockPictureUrl = _photoHelper.GetPhotoStockUrl(item.Picture);
            });

            return courses;
        }

        public async Task<CourseViewModel> GetCourseByIdAsync(string id)
        {
            var response = await _httpClient.GetAsync($"courses/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var course = (await response.Content.ReadFromJsonAsync<Response<CourseViewModel>>()).Data;

            course.StockPictureUrl = _photoHelper.GetPhotoStockUrl(course.Picture);

            return course;
        }

        public async Task<bool> UpdateCourseAsync(CourseUpdateInput courseUpdateInput)
        {
            var resultPhotoService = await _photoStockService.UploadPhoto(courseUpdateInput.PhotoFormFile);
            if (resultPhotoService != null)
            {
                await _photoStockService.DeletePhoto(courseUpdateInput.Picture);
                courseUpdateInput.Picture = resultPhotoService.Url;
            }
            var response = await _httpClient.PutAsJsonAsync("courses", courseUpdateInput);
            return response.IsSuccessStatusCode;
        }
    }
}
