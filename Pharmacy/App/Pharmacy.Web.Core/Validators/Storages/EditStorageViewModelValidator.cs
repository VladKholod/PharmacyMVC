using FluentValidation;
using Pharmacy.Web.Core.Models.Storages;

namespace Pharmacy.Web.Core.Validators.Storages
{
    public class EditStorageViewModelValidator : AbstractValidator<EditStorageViewModel>
    {
        public EditStorageViewModelValidator()
        {
            RuleFor(s => s.Quantity)
                .NotNull().WithMessage("Quantity must be filled")
                .GreaterThanOrEqualTo(0).WithMessage("Quantity must be positive");
        }
    }
}
