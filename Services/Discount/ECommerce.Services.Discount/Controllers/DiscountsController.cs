using AutoMapper;
using ECommerce.Services.Discount.Dtos;
using ECommerce.Services.Discount.Services;
using ECommerce.Shared.ControllerBases;
using ECommerce.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.Services.Discount.Controllers
{
    public class DiscountsController : CustomBaseController
    {
        private readonly IDiscountService _discountService;

        private readonly ISharedIdentityService _identityService;

        public DiscountsController(IDiscountService discountService, ISharedIdentityService identityService, IMapper mapper)
        {
            _discountService = discountService;
            _identityService = identityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => CreateActionResultInstance(await _discountService.GetAll());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) => CreateActionResultInstance(await _discountService.GetById(id));

        [HttpGet]
        [Route("/api/[controller]/[action]/{code}")]
        public async Task<IActionResult> GetByCode(string code) => CreateActionResultInstance(await _discountService.GetByCodeAndUserId(code, _identityService.GetUserId));

        [HttpPost]
        public async Task<IActionResult> Create(CreateDiscountDto dto) => CreateActionResultInstance(await _discountService.Save(dto));

        [HttpPut]
        public async Task<IActionResult> Update(UpdateDiscountDto dto) => CreateActionResultInstance(await _discountService.Update(dto));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) => CreateActionResultInstance(await _discountService.Delete(id));
    }
}
