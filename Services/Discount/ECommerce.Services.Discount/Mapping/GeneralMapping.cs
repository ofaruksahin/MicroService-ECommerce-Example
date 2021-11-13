using AutoMapper;
using ECommerce.Services.Discount.Dtos;

namespace ECommerce.Services.Discount.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Models.Discount, DiscountDto>().ReverseMap();
            CreateMap<Models.Discount, CreateDiscountDto>().ReverseMap();
            CreateMap<Models.Discount, UpdateDiscountDto>().ReverseMap();
        }
    }
}
