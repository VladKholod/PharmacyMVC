using FluentValidation;
using Pharmacy.Web.Core.Models.Storages;

namespace Pharmacy.Web.Core.Validators.Storages
{
    public class StorageViewModelValidator : AbstractValidator<StorageViewModel>
    {
        public StorageViewModelValidator()
        {
            RuleFor(s => s.Quantity)
                .NotNull().WithMessage("Quantity must be filled")
                .GreaterThanOrEqualTo(0).WithMessage("Quantity must be positive");
        }
    }
}
