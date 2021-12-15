﻿using ECommerce.Web.Models.Basket;
using ECommerce.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.Web.Controllers
{
    public class BasketController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly IBasketService _basketService;

        public BasketController(ICatalogService catalogService, IBasketService basketService)
        {
            _catalogService = catalogService;
            _basketService = basketService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _basketService.Get());
        }

        public async Task<IActionResult> AddBasketItem(string courseId)
        {
            var course = await _catalogService.GetCourseByIdAsync(courseId);
            var basketItem = new BasketItemViewModel()
            {
                CourseId = courseId,
                CourseName = course.Name,
                Price = course.Price,
                Quantity = 1
            };

            await _basketService.AddBasketItem(basketItem);
            return RedirectToAction(nameof(Index), "Basket");
        }

        public async Task<IActionResult> RemoveBasketItem(string courseId)
        {
            await _basketService.RemoveBasketItem(courseId);
            return RedirectToAction(nameof(Index), "Basket");
        }
    }
}
