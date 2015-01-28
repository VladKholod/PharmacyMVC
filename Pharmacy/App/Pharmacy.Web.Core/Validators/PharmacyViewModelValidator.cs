using System;
using FluentValidation;
using Pharmacy.Web.Core.Models;
using Pharmacy.Web.Core.Models.Pharmacies;

namespace Pharmacy.Web.Core.Validators
{
    public class PharmacyViewModelValidator : AbstractValidator<PharmacyViewModel>
    {
        public PharmacyViewModelValidator()
        {
            RuleFor(p => p.Address)
                .NotEmpty().WithMessage("Address must be filled")
                .Length(10, 100).WithMessage("Address must be at least 10 characters");
            RuleFor(p => p.Number)
                .NotNull().WithMessage("Number must be filled")
                .GreaterThanOrEqualTo(1).WithMessage("Number must be greater than 0");
            RuleFor(p => p.Phone)
                .NotEmpty().WithMessage("Phone must be filled")
                .Matches(@"^\d{3}-\d{3}-\d{2}-\d{2}$").WithMessage("Phone must be [***-***-**-**] only");
            RuleFor(p => p.OpenDate)
                .NotEmpty().WithMessage("Invalide date")
                .LessThan(DateTime.Now).WithMessage("Date must be not bigger than now");
        }
    }
}
