using ECommerce.Services.Catalog.Dtos.Course;
using ECommerce.Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Services.Catalog.Services
{
    internal interface ICourseService
    {
        Task<Response<List<CourseDto>>> GetAllAsync();
        Task<Response<CourseDto>> GetByIdAsync(string id);
        Task<Response<List<CourseDto>>> GetAllByUserId(string userId);
        Task<Response<CourseDto>> CreateAsync(CourseCreateDto dto);
        Task<Response<NoContent>> ResponseAsync(CourseUpdateDto dto);
        Task<Response<NoContent>> DeleteAsync(string id);
    }
}
