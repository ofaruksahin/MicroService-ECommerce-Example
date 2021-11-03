using ECommerce.Services.Catalog.Dtos.Category;
using ECommerce.Services.Catalog.Services;
using ECommerce.Shared.ControllerBases;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.Services.Catalog.Controllers
{
    public class CategoryController : CustomBaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() 
            => CreateActionResultInstance(await _categoryService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
            => CreateActionResultInstance(await _categoryService.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateDto dto) 
            => CreateActionResultInstance(await _categoryService.CreateAsync(dto));
    }
}
