using FluentValidation;
using Pharmacy.Web.Core.Models;

namespace Pharmacy.Web.Core.Validators
{
    public class MedicamentViewModelValidator : AbstractValidator<MedicamentViewModel>
    {
        public MedicamentViewModelValidator()
        {
            RuleFor(m => m.Description)
                .NotEmpty().WithMessage("Description must be filled")
                .Length(20,200).WithMessage("Description must be at least 20 characters");
            RuleFor(m => m.Name)
                .NotEmpty().WithMessage("Name must be filled");
            RuleFor(m => m.Price)
                .NotNull().WithMessage("Price must be filled")
                .GreaterThan(0).WithMessage("Price must be greater than 0");
            RuleFor(m => m.SerialNumber)
                .NotEmpty().WithMessage("SerialNumber must be filled")
                .Matches(@"^\d{3}-\d{3}-\d{2}$").WithMessage("SerialNumber must be [***-***-**] only");
        }
    }
}
