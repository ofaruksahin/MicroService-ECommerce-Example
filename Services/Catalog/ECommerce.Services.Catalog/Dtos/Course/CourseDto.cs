using ECommerce.Services.Catalog.Dtos.Category;
using ECommerce.Services.Catalog.Dtos.Feature;
using System;

namespace ECommerce.Services.Catalog.Dtos.Course
{
    internal class CourseDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string UserId { get; set; }
        public string Picture { get; set; }
        public DateTime CreatedTime { get; set; }
        public FeatureDto Feature { get; set; }

        public string CategoryId { get; set; }

        public CategoryDto Category { get; set; }
    }
}
