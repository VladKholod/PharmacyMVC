using System;
using FluentValidation;
using Pharmacy.Web.Core.Models;

namespace Pharmacy.Web.Core.Validators
{
    public class OrderViewModelValidator : AbstractValidator<OrderViewModel>
    {
        public OrderViewModelValidator()
        {
            RuleFor(o => o.Date)
                .NotEmpty().WithMessage("Invalide date")
                .LessThan(DateTime.Now).WithMessage("Date must be not bigger than now");
            RuleFor(o => o.PharmacyNumber)
                .NotNull().WithMessage("PharmacyNumber must be filled")
                .GreaterThan(0).WithMessage("PharmacyNumber must be greater than 0");
        }
    }
}
