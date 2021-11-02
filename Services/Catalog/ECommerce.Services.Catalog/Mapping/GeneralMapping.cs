using AutoMapper;
using ECommerce.Services.Catalog.Dtos.Category;
using ECommerce.Services.Catalog.Dtos.Course;
using ECommerce.Services.Catalog.Dtos.Feature;
using ECommerce.Services.Catalog.Models;

namespace ECommerce.Services.Catalog.Mapping
{
    internal class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Feature, FeatureDto>().ReverseMap();

            CreateMap<Course, CourseCreateDto>().ReverseMap();
            CreateMap<Course, CourseUpdateDto>().ReverseMap();

            CreateMap<Category, CategoryCreateDto>().ReverseMap();
            CreateMap<Category, CategoryUpdateDto>().ReverseMap();
        }
    }
}
