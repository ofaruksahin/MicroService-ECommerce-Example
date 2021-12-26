﻿using ECommerce.Web.Models.Orders;
using ECommerce.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ECommerce.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly IOrderService _orderService;

        public OrderController(IBasketService basketService, IOrderService orderService)
        {
            _basketService = basketService;
            _orderService = orderService;
        }

        public async Task<IActionResult> Checkout()
        {
            var basket = await _basketService.Get();

            ViewBag.basket = basket;

            return View(new CheckoutInfoInput());
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutInfoInput checkoutInfoInput)
        {
            #region Sync Scenario            
            //var orderStatus = await _orderService.CreateOrder(checkoutInfoInput);
            //if (!orderStatus.IsSuccessful)
            //{
            //    TempData["error"] = orderStatus.Error;
            //    return RedirectToAction(nameof(Checkout));
            //}

            //return RedirectToAction(nameof(SuccessfulCheckout), new
            //{
            //    orderId = orderStatus.OrderId
            //});
            #endregion
            #region Async Scenario
            var orderSuspend = await _orderService.SuspendOrder(checkoutInfoInput);
            if (!orderSuspend.IsSuccessful)
            {
                TempData["error"] = orderSuspend.Error;
                return RedirectToAction(nameof(Checkout));
            }

            return RedirectToAction(nameof(SuccessfulCheckout), new { orderId = new Random().Next(1, 1000) });
            #endregion
        }

        public IActionResult SuccessfulCheckout(int orderId)
        {
            ViewBag.orderId = orderId;
            return View();
        }

        public async Task<IActionResult> CheckoutHistory()
        {
            return View(await _orderService.GetOrder());
        }
    }
}
