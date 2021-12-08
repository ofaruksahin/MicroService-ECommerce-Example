using ECommerce.Web.Models.Catalogs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Web.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<List<CourseViewModel>> GetAllCourseAsync();
        Task<List<CategoryViewModel>> GetAllCategoriesAsync();
        Task<List<CourseViewModel>> GetAllCourseByUserIdAsync(string userId);
        Task<CourseViewModel> GetCourseByIdAsync(string id);
        Task<bool> CreateCourseAsync(CourseCreateInput courseCreateInput);
        Task<bool> UpdateCourseAsync(CourseUpdateInput courseUpdateInput);
        Task<bool> DeleteCourseAsync(string courseId);
    }
}
