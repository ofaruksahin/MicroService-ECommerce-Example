using System.ComponentModel.DataAnnotations;

namespace ECommerce.Web.Models.Catalogs
{
    public class CourseCreateInput
    {
        [Display(Name = "Kurs Adı")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Kurs Açıklaması")]
        [Required]
        public string Description { get; set; }
        [Display(Name = "Kurs Fiyatı")]
        [Required]
        public decimal Price { get; set; }
        public string UserId { get; set; }
        public string Photo { get; set; }
        public FeatureViewModel Feature { get; set; }

        [Display(Name="Kurs Kategorisi")]
        [Required]
        public string CategoryId { get; set; }
    }
}
