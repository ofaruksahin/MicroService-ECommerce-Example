﻿namespace ECommerce.Services.Discount.Dtos
{
    public class CreateDiscountDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int Rate { get; set; }
        public string Code { get; set; }
    }
}
