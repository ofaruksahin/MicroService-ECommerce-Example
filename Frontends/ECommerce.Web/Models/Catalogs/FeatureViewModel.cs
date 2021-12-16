using System.ComponentModel.DataAnnotations;

namespace ECommerce.Web.Models.Catalogs
{
    public class FeatureViewModel
    {
        [Display(Name = "Kurs süresi")]
        public int Duration { get; set; }
    }
}
