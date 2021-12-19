using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerce.Web.Models.Basket
{
    public class BasketViewModel
    {
        public string UserId { get; set; }

        public string DiscountCode { get; set; }

        public int? DiscountRate { get; set; }

        private List<BasketItemViewModel> _basketItems { get; set; }

        public List<BasketItemViewModel> BasketItems
        {
            get
            {
                if (HasDiscount)
                {
                    _basketItems.ForEach(item =>
                    {
                        var discountPrice = item.Price * ((decimal)DiscountRate.Value / 100);
                        item.AppliedDiscount(Math.Round(item.Price - discountPrice, 2));
                    });
                }
                return _basketItems;
            }
            set
            {
                _basketItems = value;
            }
        }

        public decimal TotalPrice
        {
            get => _basketItems.Sum(f => f.GetCurrentPrice);
        }

        public bool HasDiscount
        {
            get => !string.IsNullOrEmpty(DiscountCode) && DiscountRate.HasValue;
        }

        public BasketViewModel()
        {
            _basketItems = new List<BasketItemViewModel>();
        }

        public void CancelDiscount()
        {
            DiscountCode = string.Empty;
            DiscountRate = null;
        }

        public void ApplyDiscount(string code,int rate)
        {
            DiscountCode = code;
            DiscountRate =rate;
        }
    }
}
