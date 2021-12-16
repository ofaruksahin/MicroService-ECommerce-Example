using ECommerce.Web.Models.Catalogs;
using FluentValidation;

namespace ECommerce.Web.Validators
{
    public class CourseUpdateInputValidator : AbstractValidator<CourseUpdateInput>
    {
        public CourseUpdateInputValidator()
        {
            RuleFor(f => f.Name).NotEmpty().WithMessage("İsim alanı boş olamaz");
            RuleFor(f => f.Description).NotEmpty().WithMessage("Açıklama alanı boş olamaz");
            RuleFor(f => f.Feature.Duration).InclusiveBetween(1, int.MaxValue).WithMessage("Süre alanı boş olamaz");
            RuleFor(f => f.Price).NotEmpty().WithMessage("Fiyat alanı boş olamaz").ScalePrecision(2, 6).WithMessage("Hatalı para formatı");
            RuleFor(f => f.CategoryId).NotEmpty().WithMessage("Kategori alanı seçiniz");
        }
    }
}
