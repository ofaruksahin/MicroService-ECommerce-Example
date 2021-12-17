using ECommerce.Web.Models.Discounts;
using FluentValidation;

namespace ECommerce.Web.Validators
{
    public class DiscountApplyInputValidator : AbstractValidator<DiscountApplyInput>
    {
        public DiscountApplyInputValidator()
        {
            RuleFor(f => f.Code).NotEmpty().WithMessage("İndirim kuponu boş olamaz");
        }
    }
}
