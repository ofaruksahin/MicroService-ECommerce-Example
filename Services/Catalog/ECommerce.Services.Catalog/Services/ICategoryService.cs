using ECommerce.Services.Catalog.Dtos.Category;
using ECommerce.Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Services.Catalog.Services
{
    public interface ICategoryService
    {
        Task<Response<List<CategoryDto>>> GetAllAsync();
        Task<Response<CategoryDto>> CreateAsync(CategoryCreateDto dto);
        Task<Response<CategoryDto>> GetByIdAsync(string id);
    }
}
