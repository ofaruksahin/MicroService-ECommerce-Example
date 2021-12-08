using ECommerce.Shared.Dtos;
using ECommerce.Web.Models.Catalogs;
using ECommerce.Web.Services.Interfaces;
using ECommerce.Web.Settings;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ECommerce.Web.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _httpClient;

        public CatalogService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CreateCourseAsync(CourseCreateInput courseCreateInput)
        {
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

            return (await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>()).Data;
        }

        public async Task<List<CourseViewModel>> GetAllCourseByUserIdAsync(string userId)
        {
            var response = await _httpClient.GetAsync($"courses/user/{userId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return (await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>()).Data;
        }

        public async Task<CourseViewModel> GetCourseByIdAsync(string id)
        {
            var response = await _httpClient.GetAsync($"courses/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return (await response.Content.ReadFromJsonAsync<Response<CourseViewModel>>()).Data;
        }

        public async Task<bool> UpdateCourseAsync(CourseUpdateInput courseUpdateInput)
        {
            var response = await _httpClient.PutAsJsonAsync("courses", courseUpdateInput);
            return response.IsSuccessStatusCode;
        }
    }
}
