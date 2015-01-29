using FluentValidation;
using Pharmacy.Web.Core.Models.Storages;

namespace Pharmacy.Web.Core.Validators.Storages
{
    public class CreateStorageViewModelValidator : AbstractValidator<CreateStorageViewModel>
    {
        public CreateStorageViewModelValidator()
        {
            RuleFor(s => s.Quantity)
                .NotNull().WithMessage("Quantity must be filled")
                .GreaterThanOrEqualTo(0).WithMessage("Quantity must be positive");
        }
    }
}
