using System;
using FluentValidation;
using Pharmacy.Web.Core.Models.Orders;

namespace Pharmacy.Web.Core.Validators.Orders
{
    public class EditOrderViewModelValidator : AbstractValidator<EditOrderViewModel>
    {
        public EditOrderViewModelValidator()
        {
            RuleFor(o => o.Date)
                .NotEmpty().WithMessage("Invalide date")
                .LessThan(DateTime.Now).WithMessage("Date must be not bigger than now");
        }
    }
}
