using ECommerce.Services.Catalog.Dtos.Course;
using ECommerce.Services.Catalog.Services;
using ECommerce.Shared.ControllerBases;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.Services.Catalog.Controllers
{
    public class CoursesController : CustomBaseController
    {
        private readonly ICourseService _courseServices;

        public CoursesController(ICourseService courseService)
        {
            _courseServices = courseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() 
            => CreateActionResultInstance(await _courseServices.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id) 
            => CreateActionResultInstance(await _courseServices.GetByIdAsync(id));

        [Route("/api/[controller]/user/{userId}")]
        [HttpGet]
        public async Task<IActionResult> GetByUserId(string userId) 
            => CreateActionResultInstance(await _courseServices.GetAllByUserIdAsync(userId));

        [HttpPost]
        public async Task<IActionResult> Create(CourseCreateDto dto)
            => CreateActionResultInstance(await _courseServices.CreateAsync(dto));

        [HttpPut]
        public async Task<IActionResult> Update(CourseUpdateDto dto)
            => CreateActionResultInstance(await _courseServices.UpdateAsync(dto));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id) 
            => CreateActionResultInstance(await _courseServices.DeleteAsync(id));
    }
}
