using ECommerce.Shared.Services;
using ECommerce.Web.Models.Catalogs;
using ECommerce.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace ECommerce.Web.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public CoursesController(ICatalogService catalogService, ISharedIdentityService sharedIdentityService)
        {
            _catalogService = catalogService;
            _sharedIdentityService = sharedIdentityService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _catalogService.GetAllCourseByUserIdAsync(_sharedIdentityService.GetUserId));
        }

        public async Task<IActionResult> Create()
        {
            var categories = await _catalogService.GetAllCategoriesAsync();
            ViewBag.categoryList = new SelectList(categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CourseCreateInput courseCreateInput)
        {
            var categories = await _catalogService.GetAllCategoriesAsync();
            ViewBag.categoryList = new SelectList(categories, "Id", "Name");
            if (!ModelState.IsValid)
            {
                return View();
            }

            courseCreateInput.UserId = _sharedIdentityService.GetUserId;

            await _catalogService.CreateCourseAsync(courseCreateInput);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(string id)
        {
            var course = await _catalogService.GetCourseByIdAsync(id);

            var categories = await _catalogService.GetAllCategoriesAsync();
            ViewBag.categoryList = new SelectList(categories, "Id", "Name");

            if (course == null)
            {
                RedirectToAction(nameof(Index));
            }

            CourseUpdateInput courseUpdateInput = new()
            {
                Id = course.Id,
                Name = course.Name,
                CategoryId = course.CategoryId,
                Description = course.Description,
                Feature = course.Feature,
                Picture = course.Picture,
                Price = course.Price,
                UserId = course.UserId
            };
            return View(courseUpdateInput);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CourseUpdateInput courseUpdateInput)
        {
            var categories = await _catalogService.GetAllCategoriesAsync();
            ViewBag.categoryList = new SelectList(categories, "Id", "Name", courseUpdateInput.CategoryId);
            if (!ModelState.IsValid)
            {
                return View();
            }

            await _catalogService.UpdateCourseAsync(courseUpdateInput);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            await _catalogService.DeleteCourseAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
